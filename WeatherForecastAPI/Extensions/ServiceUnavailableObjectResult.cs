using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecastAPI.Extensions
{
    public class ServiceUnavailableObjectResult : ObjectResult
    {
        public ServiceUnavailableObjectResult(object value) : base(value)
        {
            this.StatusCode = (int?)HttpStatusCode.ServiceUnavailable;
        }
    }
}
