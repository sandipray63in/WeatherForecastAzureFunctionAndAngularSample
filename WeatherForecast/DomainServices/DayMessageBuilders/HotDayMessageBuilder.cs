﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Domain.WeatherForecastResponse;
using WeatherForecast.Extensions;

namespace WeatherForecast.DomainServices.DayMessageBuilders
{
    public class HotDayMessageBuilder : IDayMessageBuilder
    {
        public IList<string> GetAllMessages(IList<List> responseDataList)
        {
            IList<string> dayWeatherMessages = new List<string>();
            if (responseDataList.Any(x => x.main.temp.ConvertFromFahrenheitToCelsius() > 40))
            {
                StringBuilder sbDayWeatherMessages = new StringBuilder();
                sbDayWeatherMessages.Append("Use sunscreen lotion");
                sbDayWeatherMessages.Append("-TimeWindow[" + responseDataList.Where(x => x.main.temp.ConvertFromFahrenheitToCelsius() > 40).Select(x => x.dt_txt.Trim().Split(("").ToCharArray())[1]).Aggregate((a, b) => a + "," + b) + "]");
                dayWeatherMessages.Add(sbDayWeatherMessages.ToString());
            }
            return dayWeatherMessages;
        }
    }
}
