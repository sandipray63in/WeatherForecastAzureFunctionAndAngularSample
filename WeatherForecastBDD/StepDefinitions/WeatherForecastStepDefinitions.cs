using System;
using TechTalk.SpecFlow;
using RestSharp;
using System.Net;

namespace WeatherForecastBDD.StepDefinitions
{
    [Binding]
    public class WeatherForecastStepDefinitions
    {
        private string _authKey;
        private string _city;
        private string _numberOfDaysToForecast;
        private string _shouldIncludeToday;
        private RestResponse _restResponse;
        private HttpStatusCode _statusCode;

        [Given(@"I supply the values \('([^']*)','([^']*)', '([^']*)','([^']*)'\)")]
        public void GivenISupplyTheValues(string p0, string p1, string p2, string @true)
        {
            _authKey = p0;
            _city = p1;
            _numberOfDaysToForecast = p2;
            _shouldIncludeToday = @true;
        }

        [When(@"Weather Forecast API Executed")]
        public void WhenWeatherForecastAPIExecuted()
        {
            var headers = new Dictionary<string, string>();
            headers["auth_key"] = _authKey;
            var request = new HttpRequestWrapper()
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
