using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using WeatherForecast.Extensions;

namespace WeatherForecastTDD.Extensions
{
    [TestClass]
    public class ConverterExtensionTest
    {
        [TestMethod]
        public void TestConvertFromFahrenheitToCelsiusMethod()
        {
            //Arrange
            double temperatureInFahrenheit = 32.0;

            //Action
            double temperatureInCelcius = temperatureInFahrenheit.ConvertFromFahrenheitToCelsius();

            //Assert
            temperatureInCelcius.Should().Be(0);
        }

        [TestMethod]
        public void TestConvertFromMilesPerSecondToMilesPerHourMethod()
        {
            //Arrange
            double milesPerSecond = 1.0;

            //Action
            double milesPerHour = milesPerSecond.ConvertFromMilesPerSecondToMilesPerHour();

            //Assert
            milesPerHour.Should().Be(3600);
        }
    }
}