
Some Useful Links - 
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
https://github.com/Azure/azure-functions-openapi-extension/blob/main/docs/openapi-auth.md
https://stackoverflow.com/questions/70265255/how-to-configure-cors-in-azure-function-app
https://learn.microsoft.com/en-us/azure/azure-functions/functions-develop-local
https://stackoverflow.com/questions/63130799/azure-functions-host-json-not-applying-to-azure-application-settings

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

Azure DevOps Pipeline & ARM Templates -
https://learn.microsoft.com/en-us/powershell/azure/install-azps-windows?view=azps-10.2.0&tabs=powershell&pivots=windows-msi
https://learn.microsoft.com/en-us/azure/azure-resource-manager/bicep/install
https://learn.microsoft.com/en-us/azure/azure-resource-manager/bicep/quickstart-create-bicep-use-visual-studio?tabs=CLI
https://learn.microsoft.com/en-us/azure/devops/pipelines/create-first-pipeline?view=azure-devops&tabs=net%2Ctfs-2018-2%2Cbrowser
https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/add-template-to-azure-pipelines
https://learn.microsoft.com/en-us/samples/azure-samples/function-app-arm-templates/arm-templates-for-function-app-deployment/
https://learn.microsoft.com/en-us/azure/static-web-apps/deploy-angular?pivots=github
https://learn.microsoft.com/en-us/azure/static-web-apps/publish-azure-resource-manager?tabs=azure-cli
https://learn.microsoft.com/en-us/azure/templates/microsoft.keyvault/vaults?pivots=deployment-language-bicep

In VS 2022 Community Edition, check the containers pane & check logs pane within that & from there get the weatherforecast 
swagger url - currently its http://localhost:30486/api/swagger/ui in local env
