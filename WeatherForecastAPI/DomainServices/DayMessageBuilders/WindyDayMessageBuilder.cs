using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Domain.WeatherForecastResponse;
using WeatherForecast.Extensions;

namespace WeatherForecast.DomainServices.DayMessageBuilders
{
    public class WindyDayMessageBuilder : BaseDayMessageBuilder
    {
        private static string maxWindyDaySpeedLimit;
        public override IList<string> GetAllMessages(IList<List> responseDataList)
        {
            if (maxWindyDaySpeedLimit == null)
            {
                maxWindyDaySpeedLimit = _secretClient.GetSecret("maxWindyDaySpeedLimit").Value.Value;
            }
            IList<string> dayWeatherMessages = new List<string>();
            if (responseDataList.Any(x => x.wind.speed.ConvertFromMilesPerSecondToMilesPerHour() > Convert.ToInt32(maxWindyDaySpeedLimit)))
            {
                StringBuilder sbDayWeatherMessages = new StringBuilder();
                sbDayWeatherMessages.Append("It’s too windy, watch out!");
                sbDayWeatherMessages.Append("-TimeWindow[" + responseDataList.Where(x => x.wind.speed.ConvertFromMilesPerSecondToMilesPerHour() > 10).Select(x => x.dt_txt.Trim().Split(("").ToCharArray())[1]).Aggregate((a, b) => a + "," + b) + "]");
                dayWeatherMessages.Add(sbDayWeatherMessages.ToString());
            }
            return dayWeatherMessages;
        }
    }
}
