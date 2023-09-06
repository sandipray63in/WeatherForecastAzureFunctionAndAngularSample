using System;
using TechTalk.SpecFlow;
using RestSharp;
using System.Net;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace WeatherForecastBDD.StepDefinitions
{
    [Binding]
    public class WeatherForecastStepDefinitions
    {
        private string authKeyUnderscoreValue = "__authKey__";
        private string weatherForecastAPIUrlUnderscoreValue = "__weatherForecastAPIUrl__";
        private string _city;
        private string _numberOfDaysToForecast;
        private string _shouldIncludeToday;
        private RestResponse _restResponse;
        private HttpStatusCode _statusCode;

        [Given(@"I supply the values \('([^']*)', '([^']*)','([^']*)'\)")]
        public void GivenISupplyTheValues(string p0, string p1, string @true)
        {
            _city = p0;
            _numberOfDaysToForecast = p1;
            _shouldIncludeToday = @true;
        }

        [When(@"Weather Forecast API Executed")]
        public void WhenWeatherForecastAPIExecuted()
        {
            String jsonStr = "";
            var streamData = Assembly.GetExecutingAssembly().GetManifestResourceStream("WeatherForecastBDD.appConfig.json");
            using (StreamReader reader = new StreamReader(streamData, Encoding.UTF8))
            {
                jsonStr = reader.ReadToEnd();
            }
            AppConfig appConfig = JsonConvert.DeserializeObject<AppConfig>(jsonStr);
            var headers = new Dictionary<string, string>();
            headers["auth_key"] = appConfig.authKey.value == authKeyUnderscoreValue ? appConfig.authKey.@default : appConfig.authKey.value;
            var request = new HttpRequestWrapper()
                          .SetResourse(appConfig.weatherForecastAPIUrl.value == weatherForecastAPIUrlUnderscoreValue ? appConfig.weatherForecastAPIUrl.@default : appConfig.weatherForecastAPIUrl.value)
                          .SetMethod(Method.Get)
                          .AddHeaders(headers)
                          .AddParameter("city", _city)
                          .AddParameter("numberOfDaysToForecast", _numberOfDaysToForecast)
                          .AddParameter("shouldIncludeToday", _shouldIncludeToday);
            _restResponse = new RestResponse();
            _restResponse = request.Execute();
            _statusCode = _restResponse.StatusCode;
        }

        [Then(@"it should return '([^']*)'")]
        public void ThenItShouldReturn(string statusCode)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.Ambiguous;
            Enum.TryParse<HttpStatusCode>(statusCode, out httpStatusCode);
            _statusCode.Should().Be(httpStatusCode);
        }
    }
}
