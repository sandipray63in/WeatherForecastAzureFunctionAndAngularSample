using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Extensions
{
    public static class InvalidRequestHandlerExtensioncs
    {
        public static IList<string> GetAllInvalidRequestMessages(this HttpRequest req, string validAuthKeyValue)
        {
            var requestHeadersDictionary = req.Headers.ToDictionary(q => q.Key,q => (string)q.Value);
            string authKey = requestHeadersDictionary["auth_key"];
            string city = req.Query["city"];
            string numberOfDaysToForecast = req.Query["numberOfDaysToForecast"];
            string shouldIncludeCurrentDay = req.Query["shouldIncludeCurrentDay"];
            IList<string> allInvalidRequestMessages = new List<string>();
            if (String.IsNullOrEmpty(authKey))
            {
                allInvalidRequestMessages.Add("auth_Key header is mandatory");
            }
            if (String.IsNullOrEmpty(city))
            {
                allInvalidRequestMessages.Add("city in query string is mandatory");
            }
            if (String.IsNullOrEmpty(numberOfDaysToForecast))
            {
                allInvalidRequestMessages.Add("numberOfDaysToForecast in query string is mandatory");
            }
            if (String.IsNullOrEmpty(shouldIncludeCurrentDay))
            {
                allInvalidRequestMessages.Add("shouldIncludeCurrentDay in query string is mandatory");
            }
            if (authKey != validAuthKeyValue)
            {
                allInvalidRequestMessages.Add("invalid auth_key value provided in header");
            }
            int outNumberOfDaysToForecast = 0;
            if (!int.TryParse(numberOfDaysToForecast,out outNumberOfDaysToForecast) || outNumberOfDaysToForecast < 1)
            {
                allInvalidRequestMessages.Add("numberOfDaysToForecast provided in query string should be a valid positive number");
            }
            bool outShouldIncludeCurrentDay = false;
            if (!bool.TryParse(shouldIncludeCurrentDay, out outShouldIncludeCurrentDay))
            {
                allInvalidRequestMessages.Add("shouldIncludeCurrentDay provided in query string should be a valid boolean value(i.e. true or false)");
            }
            return allInvalidRequestMessages;
        }

        public static IList<string> GetInvalidCityMessages(this string city)
        {
            IList<string> invalidCityRequestMessages = new List<string>();
            invalidCityRequestMessages.Add("api.openweathermap.org doesn't have data for city : " + city);
            return invalidCityRequestMessages;
        }
     }
}
