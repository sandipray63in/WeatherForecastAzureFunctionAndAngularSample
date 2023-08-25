using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Domain.WeatherForecastResponse;
using WeatherForecast.Extensions;

namespace WeatherForecast.DomainServices.DayMessageBuilders
{
    public class RainyDayMessageBuilder : IDayMessageBuilder
    {
        public IList<string> GetAllMessages(IList<List> responseDataList)
        {
            IList<string> dayWeatherMessages = new List<string>();
            if (responseDataList.Any(x => x.rain != null))
            {
                StringBuilder sbDayWeatherMessages = new StringBuilder();
                sbDayWeatherMessages.Append("Carry umbrella");
                sbDayWeatherMessages.Append("-TimeWindow[" + responseDataList.Where(x => x.rain != null).Select(x => x.dt_txt.Trim().Split(("").ToCharArray())[1]).Aggregate((a, b) => a + "," + b) + "]");
                dayWeatherMessages.Add(sbDayWeatherMessages.ToString());
            }
            return dayWeatherMessages;
        }
    }
}
