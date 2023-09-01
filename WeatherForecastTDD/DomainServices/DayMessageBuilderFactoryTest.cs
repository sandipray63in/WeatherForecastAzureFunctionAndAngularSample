using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.DomainServices;

namespace WeatherForecastTDD.DomainServices
{
    [TestClass]
    public class DayMessageBuilderFactoryTest
    {
        [TestMethod]
        public void TestGetDayMessageBuildersMethod()
        {
            //Arrange
            // Do Nothing 

            //Action
            IList<IDayMessageBuilder> dayMessageBuilders = DayMessageBuilderFactory.GetDayMessageBuilders(DayMessageBuilderType.Hot, DayMessageBuilderType.Rainy, DayMessageBuilderType.Thunderstorm, DayMessageBuilderType.Windy);

            //Assert
            dayMessageBuilders.Should().HaveCount(4);
        }
    }
}
