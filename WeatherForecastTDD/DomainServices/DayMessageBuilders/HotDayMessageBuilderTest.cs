using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Domain.WeatherForecastResponse;
using WeatherForecast.DomainServices;
using WeatherForecast.DomainServices.DayMessageBuilders;
using WeatherForecast.Extensions;

namespace WeatherForecastTDD.DomainServices.DayMessageBuilders
{
    [TestClass]
    public class HotDayMessageBuilderTest
    {
        [TestMethod]
        public void TestGetAllMessagesMethod()
        {
            //Arrange
            IList<List> responseDataList = new List<List>();
            List list = new List();
            list.dt_txt = "2023-09-01 09:00:00";
            list.main = new Main();
            list.main.temp = 113;// temp in farenheit so that temp in celcius is 45 which > 40
            responseDataList.Add(list);
            IDayMessageBuilder dayMessageBuilder = new HotDayMessageBuilder();

            //Action
            IList<string> allMessages = dayMessageBuilder.GetAllMessages(responseDataList);

            //Assert
            allMessages.Should().OnlyContain(x => x.Contains("Use sunscreen lotion"));
        }
    }
}
