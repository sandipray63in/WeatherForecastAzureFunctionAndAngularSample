using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using Moq;
using WeatherForecastAPI.Extensions;

namespace WeatherForecastTDD.Extensions
{
    [TestClass]
    public class InvalidRequestHandlerExtensionTest
    {
        [TestMethod]
        public void TestGetAllInvalidRequestMessagesMethod() {
            //Arrange
            string validAuthKeyValue = "bed8f14b-a29e-44e6-b71b-036dc148fe5e";
            var mockedHttpRequest = new Mock<HttpRequest>();
            var headerDictionary = new HeaderDictionary();
            headerDictionary["auth_key"] = validAuthKeyValue;
            var paramsDictionary = new Dictionary<string, StringValues>
            {
                { "city", "kolkata" },
                { "numberOfDaysToForecast", "2" },
                { "shouldIncludeToday", "false" }
            };
            mockedHttpRequest.Setup(x => x.Headers).Returns(headerDictionary);
            mockedHttpRequest.Setup(x => x.Query).Returns(new QueryCollection(paramsDictionary));

            //Action
            IList<string> allInvalidMessages = mockedHttpRequest.Object.GetAllInvalidRequestMessages(validAuthKeyValue);

            //Assert
            allInvalidMessages.Should().BeEmpty();
        }

        [TestMethod]
        public void TestGetInvalidCityMessagesMethod()
        {
            //Arrange
            string city = "kolkata";

            //Action
            IList<string> invalidCityMessages = city.GetInvalidCityMessages();

            //Assert
            invalidCityMessages.Should().NotBeEmpty();
        }
    }
}
