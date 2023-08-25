using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Extensions
{
    public static class ConverterExtension
    {
        public static double ConvertFromFahrenheitToCelsius(this double temperatureInFahrenheit)
        {
            return ((temperatureInFahrenheit - 32) * (5 / 9));
        }

        public static double ConvertFromMilesPerSecondToMilesPerHour(this double windSpeedInMilesPerSecond)
        {
            return windSpeedInMilesPerSecond * 3600;
        }
    }
}
