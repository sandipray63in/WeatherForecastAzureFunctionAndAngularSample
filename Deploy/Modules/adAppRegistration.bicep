// Some Useful links - 
// https://practical365.com/microsoft-graph-api-permission/
// https://learn.microsoft.com/en-us/graph/migrate-azure-ad-graph-configure-permissions?tabs=http%2Cupdatepermissions-azureadgraph-powershell#see-also
// https://learn.microsoft.com/en-us/powershell/module/microsoft.graph.applications/get-mgserviceprincipal?view=graph-powershell-1.0
// https://learn.microsoft.com/en-us/graph/permissions-reference - has Subscription.Read.All
// https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/deployment-script-template-configure-dev
// https://learn.microsoft.com/en-us/azure/virtual-machines/windows/run-command
// https://learn.microsoft.com/en-us/cli/azure/vm/run-command?view=azure-cli-latest#code-try-8

param appRegistrationName string = ''
param location string = resourceGroup().location
param currentTime string = utcNow()
param userAssignedIdentityName string
param azureAplicationId string = ''
@secure()
param azureAplicationSecret string = ''

resource script 'Microsoft.Resources/deploymentScripts@2020-10-01' = {
  name: join([appRegistrationName, 'DeploymentScript'],'')
  location: location
  kind: 'AzurePowerShell'
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '${userAssignedIdentityName}': {}
    }
  }
    
  //Need to set the subscription ID as per - https://stackoverflow.com/questions/62418089/this-client-subscriptionid-cannot-be-null
  // One of the best ways to debug the below script is to run it directly in VS pwershell terminal using some test
  properties: {
    azPowerShellVersion: '5.0'
    arguments: '-tenantID ${subscription().tenantId} -subscriptionID ${subscription().subscriptionId} -azureAplicationId ${azureAplicationId} -azureAplicationSecret ${azureAplicationSecret} -resourceName "${appRegistrationName}"'
    scriptContent: '''
      param([string]$tenantID, [string]$subscriptionID,  [string]$azureAplicationId, [string]$azureAplicationSecret, [string] $resourceName)
      $azurePassword = ConvertTo-SecureString $azureAplicationSecret -AsPlainText -Force
      $psCred = New-Object System.Management.Automation.PSCredential($azureAplicationId , $azurePassword)
      Connect-AzAccount -Credential $psCred -TenantId $tenantID -ServicePrincipal 
      Set-AzContext -Subscription $subscriptionID

      $token = (Get-AzAccessToken -ResourceUrl https://graph.microsoft.com -TenantId $tenantID).Token
      $headers = @{'Content-Type' = 'application/json'; 'Authorization' = 'Bearer ' + $token}

      $template = @{
        displayName = $resourceName
        requiredResourceAccess = @(
          @{
            resourceAppId = "00000003-0000-0000-c000-000000000000" 
            resourceAccess = @(
              @{
                id = "e1fe6dd8-ba31-4d61-89e7-88639da4683d"
                type = "Scope"
              },
              @{
                id = "5f88184c-80bb-4d52-9ff2-757288b2e9b7"
                type = "Scope" 
              }
            )
          }
        )
        signInAudience = "AzureADMyOrg"
      }
      
      // Upsert App registration
      $app = (Invoke-RestMethod -Method Get -Headers $headers -Uri "https://graph.microsoft.com/beta/applications?filter=displayName eq '$($resourceName)'").value
      $principal = @{}
      if ($app) {
        $ignore = Invoke-RestMethod -Method Patch -Headers $headers -Uri "https://graph.microsoft.com/beta/applications/$($app.id)" -Body ($template | ConvertTo-Json -Depth 10)
        $principal = (Invoke-RestMethod -Method Get -Headers $headers -Uri "https://graph.microsoft.com/beta/servicePrincipals?filter=appId eq '$($app.appId)'").value
      } else {
        $app = (Invoke-RestMethod -Method Post -Headers $headers -Uri "https://graph.microsoft.com/beta/applications" -Body ($template | ConvertTo-Json -Depth 10))
        $principal = Invoke-RestMethod -Method POST -Headers $headers -Uri  "https://graph.microsoft.com/beta/servicePrincipals" -Body (@{ "appId" = $app.appId } | ConvertTo-Json)
      }
      
      // Creating client secret
      $app = (Invoke-RestMethod -Method Get -Headers $headers -Uri "https://graph.microsoft.com/beta/applications/$($app.id)")
      
      foreach ($password in $app.passwordCredentials) {
        Write-Host "Deleting secret with id: $($password.keyId)"
        $body = @{
          "keyId" = $password.keyId
        }
        $ignore = Invoke-RestMethod -Method POST -Headers $headers -Uri "https://graph.microsoft.com/beta/applications/$($app.id)/removePassword" -Body ($body | ConvertTo-Json)
      }
      
      $body = @{
        "passwordCredential" = @{
          "displayName"= "Client Secret"
        }
      }
      $secret = (Invoke-RestMethod -Method POST -Headers $headers -Uri  "https://graph.microsoft.com/beta/applications/$($app.id)/addPassword" -Body ($body | ConvertTo-Json)).secretText
      
      $DeploymentScriptOutputs = @{}
      $DeploymentScriptOutputs['objectId'] = $app.id
      $DeploymentScriptOutputs['clientId'] = $app.appId
      $DeploymentScriptOutputs['clientSecret'] = $secret
      $DeploymentScriptOutputs['principalId'] = $principal.id

    '''
    cleanupPreference: 'Always'
    retentionInterval: 'P1D'
    forceUpdateTag: currentTime // ensures script will run every time
  }
}

output objectId string = script.properties.outputs.objectId
output clientId string = script.properties.outputs.clientId
output clientSecret string = script.properties.outputs.clientSecret
output principalId string = script.properties.outputs.principalId