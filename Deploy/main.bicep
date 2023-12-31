// For deployments you can watch Azure Monitor Activity Logs as well.Check the JSON tab for any failures.
// The pipeline doesn't throw error for quite sometime & so better to cancel it and see the JSON tab in
// Azure Monitory Activity Logs
// To check all deployments, go to the Reasource Group and click Deployments.
// Also search for all Deployment Scirpts
@description('Suffix for naming resources')
param appNameSuffix string = 'app${uniqueString(resourceGroup().id)}'

param staticWebAppRegistrationName string = ''
param functionAppRegistrationName string = ''

param azureAplicationId string = ''

@secure()
param azureAplicationSecret string = ''

@allowed([
  'dev'
  'test'
  'prod'
])
@description('Environment')
param environmentType string = 'dev'

@description('Do you want to create new APIM?')
param createApim bool = true

@description('APIM name')
param apimName string = 'apim-${appNameSuffix}-${environmentType}'

@description('APIM resource group')
param apimResourceGroup string = resourceGroup().name

@description('Key Vault name')
param keyVaultName string = 'kv-${appNameSuffix}-${environmentType}'

@description('User assigned managed idenity name')
param userAssignedIdentityName string = 'umsi-${appNameSuffix}-${environmentType}'

@description('User assigned managed idenity resource group')
param userAssignedIdentityResourceGroup string = resourceGroup().name

@description('API friendly name')
param apimApiName string = 'WeatherForecastAPI'

@description('Specifies the Azure Active Directory tenant ID that should be used for authenticating requests to the key vault. Get it by using Get-AzSubscription cmdlet.')
param tenantId string = subscription().tenantId

param resourceTags object = {
  ProjectType: 'Azure Serverless Web'
  Purpose: 'PoC'
}

var location = resourceGroup().location
var staticWebsiteStorageAccountName = '${toLower(appNameSuffix)}${toLower(environmentType)}'
var cdnProfileName = 'cdn-${appNameSuffix}-${environmentType}'
var functionStorageAccountName = 'fn${appNameSuffix}${environmentType}'
var functionAppName = 'fn-${appNameSuffix}-${environmentType}'
var functionRuntime = 'dotnet'
var appServicePlanName = 'asp-${appNameSuffix}-${environmentType}'
var appInsightsName = 'ai-${appNameSuffix}-${environmentType}'

// SKUs
// https://resources.azure.com/
// https://azure.microsoft.com/en-in/explore/global-infrastructure/geographies/#geographies
// Get-AzComputeResourceSku | Where-Object { $_.ResourceTypes -contains "function"}
// https://github.com/hashicorp/terraform-provider-azurerm/issues/4658 - VVI
// https://stackoverflow.com/questions/76110029/azure-ad-app-registration-for-oauth-code-grant-flow - VVI
// https://stackoverflow.com/questions/43223072/get-list-of-skus-and-sku-capacities-for-azure-subscription-using-azure-rest-api - VVI
// https://learn.microsoft.com/en-us/answers/questions/175088/which-region-do-i-need-to-pick-to-play-with-azure
// https://learn.microsoft.com/en-us/azure/azure-functions/functions-scale
// https://techcommunity.microsoft.com/t5/fasttrack-for-azure/azure-functions-part-1-hosting-and-networking-options/ba-p/3746795
// https://stackoverflow.com/questions/47522539/server-farm-service-plan-skus - VVI
// https://learn.microsoft.com/en-us/rest/api/compute/resource-skus/list?tabs=HTTP

var functionSku = environmentType == 'prod' ? 'EP1' : 'F1'
var apimSku = environmentType == 'prod' ? 'Standard' : 'Developer'

// Use existing User Assigned MSI. See https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/deployment-script-template#configure-the-minimum-permissions
resource userAssignedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2018-11-30' existing = {
  name: userAssignedIdentityName
  scope: resourceGroup(userAssignedIdentityResourceGroup)
}

resource appInsights 'Microsoft.Insights/components@2018-05-01-preview' = {
  name: appInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}

module staticWebADAppReg 'Modules/adAppRegistration.bicep' = {
  name: 'staticWebADAppReg'
  scope: resourceGroup(apimResourceGroup)
  params: {
    userAssignedIdentityName: userAssignedIdentityName
    azureAplicationId: azureAplicationId
    azureAplicationSecret: azureAplicationSecret
    appRegistrationName:staticWebAppRegistrationName
  }
}

module staticWebsite 'Modules/staticWebsite.bicep' = {
  name: 'staticWebsite'
  params: {
    storageAccountName: staticWebsiteStorageAccountName
    deploymentScriptServicePrincipalId: userAssignedIdentity.id
    azureAplicationId: azureAplicationId
    azureAplicationSecret: azureAplicationSecret
    resourceTags: resourceTags
  }
}

module cdn 'Modules/cdn.bicep' = {
  name: 'cdn'
  params: {
    cdnProfileName: cdnProfileName
    staticWebsiteURL: staticWebsite.outputs.staticWebsiteURL
  }
}

module apimApi 'Modules/apimAPI.bicep' = {
  name: 'apimAPI'
  scope: resourceGroup(apimResourceGroup)
  params: {
    apimName: apimName
    currentResourceGroup: resourceGroup().name
    backendApiName: functionApp.outputs.functionAppName
    apiName: apimApiName
    originUrl: cdn.outputs.cdnEndpointURL
  }
}

module functionADAppReg 'Modules/adAppRegistration.bicep' = {
  name: 'functionADAppReg'
  scope: resourceGroup(apimResourceGroup)
  params: {
    userAssignedIdentityName: userAssignedIdentityName
    azureAplicationId: azureAplicationId
    azureAplicationSecret: azureAplicationSecret
    appRegistrationName:functionAppRegistrationName
  }
}

module keyVault 'Modules/keyVault.bicep' = {
  name: 'keyVault'
  scope: resourceGroup(apimResourceGroup)
  params: {
    keyVaultName: keyVaultName
    objectId: functionADAppReg.outputs.objectId
  }
}

module functionApp 'Modules/function.bicep' = {
  name: 'functionApp'
  params: {
    location: resourceGroup().location
    functionRuntime: functionRuntime
    functionSku: functionSku
    storageAccountName: functionStorageAccountName
    functionAppName: functionAppName
    appServicePlanName: appServicePlanName
    appInsightsInstrumentationKey: appInsights.properties.InstrumentationKey
    staticWebsiteURL: staticWebsite.outputs.staticWebsiteURL
    apimIPAddress: apim.outputs.apiIPAddress
    resourceTags: resourceTags
    keyVaultUrl: keyVault.outputs.keyVaultUrl
    azureTenantId: tenantId
    azureClientId: functionADAppReg.outputs.clientId
    azureClientSecret: functionADAppReg.outputs.clientSecret
  }
}

module apim 'Modules/apim.bicep' = if (createApim) {
  name: 'apim'
  params: {
    apimName: apimName
    appInsightsName: appInsightsName
    appInsightsInstrumentationKey: appInsights.properties.InstrumentationKey
    sku: apimSku
    resourceTags: resourceTags
  }
}

output functionAppName string = functionApp.outputs.functionAppName
output apiUrl string = '${apim.outputs.gatewayUrl}/${apimApiName}'
output staticWebsiteStorageAccountName string = staticWebsiteStorageAccountName
output staticWebsiteUrl string = staticWebsite.outputs.staticWebsiteURL
output apimName string = apimName
output cdnEndpointName string = cdn.outputs.cdnEndpointName
output cdnProfileName string = cdn.outputs.cdnProfileName
output cdnEndpointURL string = cdn.outputs.cdnEndpointURL
output authKey string = keyVault.outputs.authKey
output resourceGroupName string = resourceGroup().name
output apiName string = apimApiName