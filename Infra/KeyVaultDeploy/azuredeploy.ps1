Connect-AzAccount

New-AzResourceGroup -Name WeatherForecastRG -Location centralindia

New-AzResourceGroupDeployment -ResourceGroupName WeatherForecastRG -TemplateFile .\main.bicep -TemplateParameterFile .\azuredeploy.parameters.json