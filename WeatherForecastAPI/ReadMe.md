
Some Useful Links - 
Some Good Azure Samples -      
https://github.com/Azure-Samples/contoso-real-estate    
https://github.com/Azure-Samples/serverless-web-application    

Docker -     
https://stackoverflow.com/questions/53521104/asp-net-core-the-project-doesnt-know-how-to-run-the-profile-docker-on-visua     
https://stackoverflow.com/questions/51252181/how-to-start-docker-daemon-windows-service-at-startup-without-the-need-to-log     
https://stackoverflow.com/questions/71084718/docker-desktop-stopped-message-after-installation     
https://www.appsdeveloperblog.com/docker-desktop-stopped-error/#google_vignette     

Azure Functions -    
https://learn.microsoft.com/en-us/azure/azure-functions/openapi-apim-integrate-visual-studio     
https://stackoverflow.com/questions/46315734/how-to-call-another-function-with-in-an-azure-function     
https://roykim.ca/2023/02/19/how-to-create-an-azure-function-app-that-calls-an-external-3rd-party-api/    
https://dev.to/asizikov/fixing-console-logs-for-azure-functions-running-in-a-docker-container-1lod     
https://stackoverflow.com/questions/70265255/how-to-configure-cors-in-azure-function-app     
https://learn.microsoft.com/en-us/azure/azure-functions/functions-develop-local     
https://stackoverflow.com/questions/63130799/azure-functions-host-json-not-applying-to-azure-application-settings     

Security -    
https://learn.microsoft.com/en-us/azure/active-directory/develop/v2-oauth2-implicit-grant-flow
https://learn.microsoft.com/en-us/cli/azure/create-an-azure-service-principal-azure-cli
https://learn.microsoft.com/en-us/azure/api-management/howto-protect-backend-frontend-azure-ad-b2c      
https://github.com/AzureAD/microsoft-authentication-library-for-js/tree/dev  
https://github.com/Azure/azure-functions-openapi-extension
https://learn.microsoft.com/en-us/azure/api-management/authentication-authorization-overview
https://learn.microsoft.com/en-us/azure/api-management/api-management-policies
https://learn.microsoft.com/en-us/azure/well-architected/services/networking/api-management/operational-excellence
https://learn.microsoft.com/en-us/azure/architecture/example-scenario/apps/publish-internal-apis-externally
https://medium.com/azure-architects/azure-api-management-and-application-gateway-integration-a31fde80f3db
https://arinco.com.au/blog/internal-external-apim-app-gateway/
https://learn.microsoft.com/en-us/azure/active-directory/external-identities/b2b-fundamentals


TDD & BDD -     
https://mahmutcanga.com/2019/12/13/unit-testing-httprequest-in-c/      
https://methodpoet.com/unit-test-httpclient/      
https://techcommunity.microsoft.com/t5/testingspot-blog/what-is-bdd-how-to-use-specflow-in-visual-studio-2022-specflow/ba-p/3255140      
https://stackoverflow.com/questions/17167820/specflow-error-force-regenerate-steps-possible      
https://www.codeproject.com/Articles/1086520/Using-Specflow-to-Test-Web-API-PART     
https://stackoverflow.com/questions/25112766/specflow-calling-steps-within-steps-causes-no-matching-step-definition-error      

Key Vault & DefaultCredential -         
https://learn.microsoft.com/en-us/answers/questions/824221/how-to-use-key-vault-references-for-azure-function       
https://learn.microsoft.com/en-us/answers/questions/1164658/how-we-can-secure-the-local-setting-json-file-insi      
https://stackoverflow.com/questions/61741877/angular-azure-key-vault-managing-vault-access-secrets     
https://learn.microsoft.com/en-us/azure/key-vault/secrets/quick-create-node?tabs=windows      
https://stackoverflow.com/questions/48856782/getting-error-failed-to-decrypt-settings-when-trying-to-start-azure-bot-fun      
https://www.c-sharpcorner.com/article/defaultazureidentity-and-its-various-credential-types3/     
https://stackoverflow.com/questions/75086474/defaultazurecredetials-cant-authenticate-via-visual-studio-cant-find-azurese      
https://www.rahulpnath.com/blog/defaultazurecredential-from-azure-sdk/     
https://github.com/microsoft/DockerTools/issues/345     
https://github.com/Azure/azure-sdk-for-net/issues/19167      
https://www.wardvanbesien.info/post/using-key-vault-locally-simpler/       
https://github.com/Azure/azure-sdk-for-net/issues/26652        
https://docs.privacera.com/4.3/encryption-ug/connect_to_key_vault_with_client_id_and_secret/#:~:text=ID%20and%20secret.-,Generate%20the%20Client%20ID,of%20the%20Vault%20URI%20AZURE_KEYVAULT_URL.       
Best option for DefaultAzureCredential to work with Visual Studio Local, Docker Local and Azure Deployment seems to use EnvironmentCredential
by setting AZURE_TENANT_ID, AZURE_CLIENT_ID and AZURE_CLIENT_SECRET in local.settings.json or Azure Function Env variables.By the way,
Docker works only with EnvironmentCredential       

GitHub Actions, Azure DevOps Pipeline & Bicep(/ARM) Templates -          
https://learn.microsoft.com/en-us/azure/devops/pipelines/architectures/devops-pipelines-baseline-architecture?view=azure-devops        
https://github.com/Azure/actions-workflow-samples         
https://azure.github.io/actions/        
https://www.shanebart.com/deploy-az-func-with-github-actions/ - create an azure AD App Registration based service principal
and use that as Github secret & name it as AZURE_CREDENTIALS
https://github.com/Azure/login/issues/205
https://colinsalmcorner.com/actions-authenticate-to-azure-without-a-secret/
https://wallis.dev/blog/composite-github-actions
https://dev.to/n3wt0n/github-composite-actions-nest-actions-within-actions-3e5l
https://stackoverflow.com/questions/74350826/github-composite-actions-cant-find-action-yml
https://learn.microsoft.com/en-us/graph/migrate-azure-ad-graph-configure-permissions?tabs=http%2Cupdatepermissions-azureadgraph-powershell
https://docs.github.com/en/actions/using-workflows/reusing-workflows#using-outputs-from-a-reusable-workflow       
https://github.com/MicrosoftDocs/azure-docs/blob/main/articles/azure-resource-manager/bicep/outputs.md     
https://learn.microsoft.com/en-us/azure/templates/microsoft.resources/deploymentscripts?pivots=deployment-language-bicep      
https://docs.github.com/en/actions/automating-builds-and-tests/building-and-testing-net      
https://dev.to/this-is-learning/manually-trigger-a-github-action-with-workflowdispatch-3mga      
https://learn.microsoft.com/en-us/dotnet/architecture/devops-for-aspnet-developers/actions-index      
https://stackoverflow.com/questions/64055230/nested-templates-calling-a-yaml-file-from-another-yaml-file-in-github-actions       
https://stackoverflow.com/questions/62750603/github-actions-trigger-another-action-after-one-action-is-completed       
https://learn.microsoft.com/en-us/azure/devops/pipelines/ecosystems/dotnet-core?view=azure-devops&tabs=dotnetfive      
https://learn.microsoft.com/en-us/azure/architecture/serverless/guide/serverless-app-cicd-best-practices     
https://stackoverflow.com/questions/57706075/how-to-call-yml-script-from-other-yml-in-azure-devops     
https://learn.microsoft.com/en-us/azure/devops/pipelines/process/pipeline-triggers?view=azure-devops      
https://learn.microsoft.com/en-us/azure/static-web-apps/build-configuration?tabs=azure-devops     
https://learn.microsoft.com/en-us/azure/devops/pipelines/tasks/reference/azure-powershell-v5?view=azure-pipelines       
https://stackoverflow.com/questions/69120936/how-do-i-use-bicep-or-arm-to-create-an-ad-app-registration-and-roles       
https://github.com/shayki5/azure-devops-create-pr-task       
https://learn.microsoft.com/en-us/azure/devops/pipelines/release/azure-key-vault?view=azure-devops&tabs=yaml      
https://learn.microsoft.com/en-us/powershell/azure/install-azps-windows?view=azps-10.2.0&tabs=powershell&pivots=windows-msi       
https://learn.microsoft.com/en-us/azure/azure-resource-manager/bicep/install      
https://learn.microsoft.com/en-us/azure/azure-resource-manager/bicep/quickstart-create-bicep-use-visual-studio?tabs=CLI       
https://learn.microsoft.com/en-us/azure/devops/pipelines/create-first-pipeline?view=azure-devops&tabs=net%2Ctfs-2018-2%2Cbrowser       
https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/add-template-to-azure-pipelines        
https://learn.microsoft.com/en-us/samples/azure-samples/function-app-arm-templates/arm-templates-for-function-app-deployment/     
https://learn.microsoft.com/en-us/azure/static-web-apps/deploy-angular?pivots=github      
https://learn.microsoft.com/en-us/azure/static-web-apps/publish-azure-resource-manager?tabs=azure-cli     
https://learn.microsoft.com/en-us/azure/templates/microsoft.keyvault/vaults?pivots=deployment-language-bicep      
https://learn.microsoft.com/en-us/azure/azure-functions/run-functions-from-deployment-package     
https://stackoverflow.com/questions/1153126/how-to-create-a-zip-archive-with-powershell      
https://stackoverflow.com/questions/65703350/azure-devops-not-running-ms-test-unit-tests      
https://docs.specflow.org/projects/specflow-livingdoc/en/latest/sbsguides/sbsazdo.html      
https://github.com/marketplace/actions/sonarscan-dotnet      
In Azure DevOps, Build Pipeline is mainly for CI while Release Pipeline is mainly for CD.
If you are using a self hosted runner then use "runs-on: self-hosted" in your yaml workflows else use the dedicated runners from github like windows-latest or ubuntu-latest etc.
But self-hosted runners seems pretty slow.

In VS 2022 Community Edition, check the containers pane & check logs pane within that & from there get the weatherforecast 
swagger url - currently its http://localhost:30486/api/swagger/ui in local env



az vm run-command invoke  --command-id RunPowerShellScript --name TestVM -g WeatherForecast-dev-rg  \
    --scripts '''
    param([string] $subscriptionID, [string] $storageAccount, [string] $resourceGroup)
      Select-AzSubscription -SubscriptionId $subscriptionID
      $storage = Get-AzStorageAccount -ResourceGroupName $resourceGroup -Name $storageAccount
      $ctx = $storage.Context
      Enable-AzStorageStaticWebsite -Context $ctx -IndexDocument index.html
      $output = $storage.PrimaryEndpoints.Web
      $output = $output.TrimEnd('/')
      $DeploymentScriptOutputs = @{}
      $DeploymentScriptOutputs['URL'] = $output
    ''' \
    --parameters 'subscriptionID=53ea2b06-4149-446b-9094-d6deeade43b5' 'storageAccount=weatherforecastdev' 'resourceGroup=WeatherForecast-dev-rg'
