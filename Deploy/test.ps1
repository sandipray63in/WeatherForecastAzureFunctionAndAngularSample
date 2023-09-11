#can test with PS D:\WeatherSolution\Deploy .\test.ps1 53ea2b06-4149-446b-9094-d6deeade43b5 weatherforecastdev WeatherForecast-dev-rg

 [Parameter(Mandatory=$true)]
 [string]$subscriptionID = $args[0]

 [Parameter(Mandatory=$true)]
 [string]$storageAccount = $args[1]

 [Parameter(Mandatory=$true)]
 [string]$resourceGroup = $args[2]

 Get-AzContext -ListAvailable | Where{$_.Name -match $subscriptionID} | Set-AzContext
 echo '1'
 $storage = Get-AzStorageAccount -ResourceGroupName $resourceGroup -Name $storageAccount
 echo '2'
 echo $storage
 $ctx = $storage.Context
 echo '3'
 echo $ctx
 Enable-AzStorageStaticWebsite -Context $ctx -IndexDocument index.html
 echo '4'
 $output = $storage.PrimaryEndpoints.Web
 echo '5'
 echo $output
 $output = $output.TrimEnd('/')
 echo '6'
 echo $output
 $DeploymentScriptOutputs = @{}
 echo '7'
 echo $DeploymentScriptOutputs
 $DeploymentScriptOutputs['URL'] = $output
 echo '8'
 echo $DeploymentScriptOutputs['URL']