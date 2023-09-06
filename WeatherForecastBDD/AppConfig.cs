using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecastBDD
{
    public class AppConfig
    {
        public WeatherForecastAPIUrl? weatherForecastAPIUrl { get; set; }
        public AuthKey? authKey { get; set; }

        public class AuthKey
        {
            public string? value { get; set; }
            public string? @default { get; set; }
        }

        public class WeatherForecastAPIUrl
        {
            public string? value { get; set; }
            public string? @default { get; set; }
        }
    }
}
