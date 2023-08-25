using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Domain
{
    public class DayHighLowTempAndMessages
    {
        public string DayDate { get; set; }

        public double DayHighTemperature { get; set; }

        public double DayLowTemperature { get; set; }

        public IList<string> DayWeatherMessages { get; set; }
    }
}
