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
  // Need to add Connect-AzAccount else SubscriptionId doesnt get fetched properly
  properties: {
    azPowerShellVersion: '6.1'
    timeout: 'PT30M'
    arguments: '-userAssignedIdentityName "${userAssignedIdentityName}" -storageAccount ${storageAccount.name} -resourceGroup ${resourceGroup().name}'
    scriptContent: '''
      param([string]$userAssignedIdentityName, [string] $storageAccount, [string] $resourceGroup) 
      Set-PSRepository -Name "PSGallery" -InstallationPolicy Trusted
      Install-Module -Name Az.ManagedServiceIdentity -RequiredVersion 0.7.3 --yes
      $identity = Get-AzUserAssignedIdentity -ResourceGroupName $resourceGroup -Name $userAssignedIdentityName
      Get-AzVM -ResourceGroupName contoso -Name testvm | Update-AzVM -IdentityType UserAssigned -IdentityId $identity.Id
      Connect-AzAccount -Identity -AccountId $identity.ClientId # Run on the virtual machine
      Select-AzSubscription -SubscriptionId $subscriptionID 
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
