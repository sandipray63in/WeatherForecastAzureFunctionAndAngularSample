// Some Useful links - 
// https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/deployment-script-template-configure-dev
// https://learn.microsoft.com/en-us/azure/virtual-machines/windows/run-command
// https://learn.microsoft.com/en-us/cli/azure/vm/run-command?view=azure-cli-latest#code-try-8

param storageAccountName string
param resourceTags object
param deploymentScriptServicePrincipalId string
param currentTime string = utcNow()
param userAssignedIdentityName string
var location = resourceGroup().location 

resource storageAccount 'Microsoft.Storage/storageAccounts@2019-06-01' = {
  name: storageAccountName
  location: location
  tags: resourceTags
  sku: {
    name: 'Standard_LRS'
    tier: 'Standard'
  }
  kind: 'StorageV2'
  properties: {
    supportsHttpsTrafficOnly: true
    encryption: {
      services: {
        file: {
          keyType: 'Account'
          enabled: true
        }
        blob: {
          keyType: 'Account'
          enabled: true
        }
      }
      keySource: 'Microsoft.Storage'
    }
    accessTier: 'Hot'
  }
}

resource deploymentScripts 'Microsoft.Resources/deploymentScripts@2020-10-01' = {
  name: 'configStaticWeb'
  kind: 'AzurePowerShell'
  location: location
  identity:{
    type: 'UserAssigned'
    userAssignedIdentities: {
      '${deploymentScriptServicePrincipalId}': {}
    }
  }

  //Need to set the subscription ID as per - https://stackoverflow.com/questions/62418089/this-client-subscriptionid-cannot-be-null
  // https://stackoverflow.com/questions/60632474/no-subscription-found-in-the-context-error-when-invoking-azure-powershell-cmdl
  properties: {
    azPowerShellVersion: '6.1'
    timeout: 'PT30M'
    arguments: '-tenantID ${subscription().tenantId}  -subscriptionID  ${subscription().subscriptionId} -storageAccount ${storageAccount.name} -resourceGroup ${resourceGroup().name}'
    scriptContent: '''
      param([string]$tenantID, [string]$subscriptionID, [string] $storageAccount, [string] $resourceGroup) 
      $azureAplicationId = '06c1b704-d507-412b-ad01-b8c01d2afabd'
      $azurePassword = ConvertTo-SecureString 'Qcb8Q~ouXn3x3A4uoqZneLLOph8VQEifEkhZPcUK' -AsPlainText -Force
      $psCred = New-Object System.Management.Automation.PSCredential($azureAplicationId , $azurePassword)
      Connect-AzAccount -Credential $psCred -TenantId $tenantID -ServicePrincipal 
      Set-AzContext -Subscription $subscriptionID
      $storage = Get-AzStorageAccount -ResourceGroupName $resourceGroup -Name $storageAccount 
      $ctx = $storage.Context 
      Enable-AzStorageStaticWebsite -Context $ctx -IndexDocument index.html 
      $output = $storage.PrimaryEndpoints.Web 
      $output = $output.TrimEnd('/') 
      $DeploymentScriptOutputs = @{} 
      $DeploymentScriptOutputs['URL'] = $output 
    '''
    cleanupPreference: 'Always'
    retentionInterval: 'P1D'
    forceUpdateTag: currentTime // ensures script will run every time
  }
}

output staticWebsiteURL string = deploymentScripts.properties.outputs.URL
