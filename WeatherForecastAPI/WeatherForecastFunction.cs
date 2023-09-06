using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using WeatherForecastAPI.Domain;
using WeatherForecastAPI.Domain.WeatherForecastResponse;
using WeatherForecastAPI.DomainServices;
using WeatherForecastAPI.Extensions;

namespace WeatherForecastAPI
{
    public class WeatherForecastFunction
    {
        private const string cityNotFoundMessage = "city not found";
        private const string formattedCityNotFoundMessage = "city {0} not found";
        private static SecretClient _secretClient;
        private static string key_vault_url = Environment.GetEnvironmentVariable("KEY_VAULT_URL");
        private static string auth_key;
        private static string apiUrl;
        private HttpClient _httpClient;
        private readonly ILogger<WeatherForecastFunction> _logger;

        public WeatherForecastFunction(ILogger<WeatherForecastFunction> log)
        {
            _logger = log;
        }

        public void SetHttpClient (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public  void SetSecretClient(SecretClient secretClient)
        {
            _secretClient = secretClient;
        }

        [FunctionName("WeatherForecast")]
        [OpenApiOperation(operationId: "GetWeatherForecastData", tags: new[] { "WeatherForecastData" })]
        [OpenApiSecurity("function_auth_Key", SecuritySchemeType.ApiKey, Name = "auth_key", In = OpenApiSecurityLocationType.Header)]
        [OpenApiParameter(name: "city", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The city's name for which the weather data needs to be fetched")]
        [OpenApiParameter(name: "numberOfDaysToForecast", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The next number of days of data to be forecasted")]
        [OpenApiParameter(name: "shouldIncludeToday", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Whether current day's date should be included as part of the forecast")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IList<DayHighLowTempAndMessages>), Description = "The Weather Forecast data")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(IList<string>), Description = "Invalid Request Data Messages")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.ServiceUnavailable, contentType: "application/json", bodyType: typeof(string), Description = "Open API Service Unavailable Message")]
        public async Task<IActionResult> GetWeatherForecastData(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            TokenCredential azureCredential = new DefaultAzureCredential();
            _logger.LogInformation("azureCredential is : " + JsonConvert.SerializeObject(azureCredential));

            if (_secretClient == null){
                _secretClient = new SecretClient(vaultUri: new Uri(key_vault_url), credential: azureCredential);
            }
            //_logger.LogInformation("secretClient is : " + JsonConvert.SerializeObject(_secretClient));

            if (auth_key == null){
                auth_key = _secretClient.GetSecret("authKey").Value.Value;
            }
            if (apiUrl == null){
                apiUrl = _secretClient.GetSecret("openWeatherMapApiUrl").Value.Value;
            }
            _logger.LogInformation("auth_key is : " + auth_key);
            _logger.LogInformation("apiUrl is : " + apiUrl);

            var requestHeadersDictionary = req.Headers.ToDictionary(q => q.Key, q => (string)q.Value);
            _logger.LogInformation("requestHeadersDictionary is : " + JsonConvert.SerializeObject(requestHeadersDictionary));
            IList<string> allInvalidRequestMessages = req.GetAllInvalidRequestMessages(auth_key);
            if(allInvalidRequestMessages.Count > 0)
            {
                BadRequestObjectResult badRequestObjectResult = new BadRequestObjectResult(allInvalidRequestMessages);
                return await Task.FromResult(badRequestObjectResult).ConfigureAwait(false);
            }
            
            string authKey = requestHeadersDictionary["auth_key"];
            string city = req.Query["city"];
            string numberOfDaysToForecast = req.Query["numberOfDaysToForecast"];
            string shouldIncludeToday = req.Query["shouldIncludeToday"];

            int numberOfDaysToForecastInt = 0;
            int.TryParse(numberOfDaysToForecast,out numberOfDaysToForecastInt);
            if(shouldIncludeToday == "true")
            {
                numberOfDaysToForecastInt += 1;
            }
            int openWeatherMapCntValue = numberOfDaysToForecastInt * 8;

            string formattedUrl = string.Format(apiUrl, city, openWeatherMapCntValue);
            _logger.LogInformation("formattedUrl is : " + formattedUrl);

            //setup http client & get response
            if(_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
            HttpResponseMessage response = null;
            string jsonData = string.Empty;
            try
            {
                response = await _httpClient.GetAsync(formattedUrl);
                jsonData = await response.Content.ReadAsStringAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception occurred : " + ex.ToString());
                string openWeatherMapAPIUnavailableMessage = "api.openweathermap.org is currently unavailable";
                ServiceUnavailableObjectResult serviceUnavailableObjectResult = new ServiceUnavailableObjectResult(openWeatherMapAPIUnavailableMessage);
                return await Task.FromResult(serviceUnavailableObjectResult).ConfigureAwait(false);
            }

            //deserialize the http response
            WeatherForecastResponseData responseData = JsonConvert.DeserializeObject<WeatherForecastResponseData>(jsonData);
            if(responseData.message.Trim() == cityNotFoundMessage)
            {
                BadRequestObjectResult badRequestObjectResult = new BadRequestObjectResult(string.Format(formattedCityNotFoundMessage,city));
                return await Task.FromResult(badRequestObjectResult).ConfigureAwait(false);
            }
            IList<List> responseDataList = responseData.list;
            if (shouldIncludeToday == "false")
            {
                String currentDateText = DateTime.Now.ToString("yyyy-MM-dd");
                responseDataList = responseDataList.Where(x => !x.dt_txt.Trim().StartsWith(currentDateText)).ToList();
            }
            ConcurrentBag<DayHighLowTempAndMessages> dayHighLowTempAndMessagesBag = new ConcurrentBag<DayHighLowTempAndMessages>();
            IList<string> distinctDates = responseDataList.Select(x => x.dt_txt.Trim().Split((" ").ToCharArray())[0].Trim()).Distinct().ToList();
            IList<IDayMessageBuilder> dayMessageBuilders = DayMessageBuilderFactory.GetDayMessageBuilders(DayMessageBuilderType.Rainy, DayMessageBuilderType.Hot, DayMessageBuilderType.Windy, DayMessageBuilderType.Thunderstorm);
            Parallel.ForEach(distinctDates, distinctDate =>
            {
                DayHighLowTempAndMessages dayHighLowTempAndMessages = new DayHighLowTempAndMessages();
                IList<List> responseDataListForCurretDistinctDate = responseDataList.Where(x => x.dt_txt.Trim().StartsWith(distinctDate)).ToList();
                dayHighLowTempAndMessages.DayDate = distinctDate;
                dayHighLowTempAndMessages.DayLowTemperature = responseDataList.Where(x => x.dt_txt.Trim().StartsWith(distinctDate)).Min(x => Convert.ToDouble(x.main.temp_min));
                dayHighLowTempAndMessages.DayHighTemperature = responseDataList.Where(x => x.dt_txt.Trim().StartsWith(distinctDate)).Max(x => Convert.ToDouble(x.main.temp_max));
                foreach(IDayMessageBuilder dayMessageBuilder in dayMessageBuilders)
                {
                    IList<string> dayMessageBuilderMessages = dayMessageBuilder.GetAllMessages(responseDataListForCurretDistinctDate).Where(x => !String.IsNullOrEmpty(x)).ToList();
                    foreach(string dayMessageBuilderMessage in dayMessageBuilderMessages) {
                        if (dayHighLowTempAndMessages.DayWeatherMessages == null)
                        {
                            dayHighLowTempAndMessages.DayWeatherMessages = new List<string>();
                        }
                        dayHighLowTempAndMessages.DayWeatherMessages.Add(dayMessageBuilderMessage); 
                    }
                }
                dayHighLowTempAndMessagesBag.Add(dayHighLowTempAndMessages);
            });
            List<DayHighLowTempAndMessages> dayHighLowTempAndMessagesList = dayHighLowTempAndMessagesBag.ToList();
            return new OkObjectResult(dayHighLowTempAndMessagesList.OrderByDescending(x => Convert.ToDateTime(x.DayDate)).Skip(dayHighLowTempAndMessagesList.Count - numberOfDaysToForecastInt).ToList());
        }
    }
}

