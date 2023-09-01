using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Domain.WeatherForecastResponse;
using WeatherForecast.DomainServices.DayMessageBuilders;
using WeatherForecast.DomainServices;

namespace WeatherForecastTDD.DomainServices.DayMessageBuilders
{
    [TestClass]
    public class WindyDayMessageBuilderTest
    {
        [TestMethod]
        public void TestGetAllMessagesMethod()
        {
            //Arrange
            IList<List> responseDataList = new List<List>();
            List list = new List();
            list.dt_txt = "2023-09-01 09:00:00";
            list.wind = new Wind();
            list.wind.speed = 1;// wind speed in miles per second is kept at 1 so that wind speed in miles per hour is 3600 which > 10
            responseDataList.Add(list);
            IDayMessageBuilder dayMessageBuilder = new WindyDayMessageBuilder();

            //Action
            IList<string> allMessages = dayMessageBuilder.GetAllMessages(responseDataList);

            //Assert
            allMessages.Should().OnlyContain(x => x.Contains("It’s too windy, watch out!"));
        }
    }
}
