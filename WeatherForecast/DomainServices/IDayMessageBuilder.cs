using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Domain.WeatherForecastResponse;

namespace WeatherForecast.DomainServices
{
    public interface IDayMessageBuilder
    {
       IList<string> GetAllMessages(IList<List> responseDataList);
    }
}
