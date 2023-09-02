using FluentAssertions;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast;
using WeatherForecast.DomainServices;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Extensions;
using System.Reflection;
using System.IO;
using Moq.Protected;
using System.Net;

namespace WeatherForecastTDD
{
    [TestClass]
    public class WeatherForecastFunctionTest
    {
        private Mock<ILogger<WeatherForecastFunction>>? weatherForecastFunctionLoggerMock;
        private WeatherForecastFunction? weatherForecastFunction;
        private string? validAuthKeyValue;
        private Mock<HttpRequest>? mockedHttpRequest;
        private Dictionary<string, StringValues>? paramsDictionary;
        private Mock<HttpMessageHandler>? mockedHttpMessageHandler;
        private HttpClient? httpClient;

        [TestInitialize]
        public void Initialize()
        {
            //Arrange
            mockedHttpMessageHandler = new Mock<HttpMessageHandler>();
            httpClient = new HttpClient(mockedHttpMessageHandler.Object);
            weatherForecastFunctionLoggerMock = new Mock<ILogger<WeatherForecastFunction>>();
            weatherForecastFunction = new WeatherForecastFunction(weatherForecastFunctionLoggerMock.Object);
            weatherForecastFunction.SetHttpClient(httpClient);

            validAuthKeyValue = "bed8f14b-a29e-44e6-b71b-036dc148fe5e";
            mockedHttpRequest = new Mock<HttpRequest>();
            var headerDictionary = new HeaderDictionary();
            headerDictionary["auth_key"] = validAuthKeyValue;
            paramsDictionary = new Dictionary<string, StringValues>
            {
                { "city", "" },
                { "numberOfDaysToForecast", "2" },
                { "shouldIncludeToday", "false" }
            };
            mockedHttpRequest.Setup(x => x.Headers).Returns(headerDictionary);
            mockedHttpRequest.Setup(x => x.Query).Returns(new QueryCollection(paramsDictionary));
        }

        [TestMethod]
        public async Task TestGetWeatherForecastDataMethodForInvalidRequestMessages()
        {
            //Action
            IActionResult actionaResultTask = await weatherForecastFunction?.GetWeatherForecastData(mockedHttpRequest?.Object);

            //Assert
            actionaResultTask.Should().As<BadRequestObjectResult>();
        }

        [TestMethod]
        public async Task TestGetWeatherForecastDataMethodForServiceUnavailableAsync()
        {
            //Arrange 
            paramsDictionary["city"] = "kolkata";
            mockedHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ThrowsAsync(new Exception())
            .Verifiable();

            //Action
            IActionResult actionaResultTask = await weatherForecastFunction?.GetWeatherForecastData(mockedHttpRequest?.Object);

            //Assert
            actionaResultTask.Should().As<ServiceUnavailableObjectResult>();
        }

        [TestMethod]
        public async Task TestGetWeatherForecastDataMethodForCityNotFound()
        {
            //Arrange 
            String jsonStr = "";
            var streamData = Assembly.GetExecutingAssembly().GetManifestResourceStream("WeatherForecastTDD.CityNotFoundFake.json");
            using (StreamReader reader = new StreamReader(streamData, Encoding.UTF8))
            {
                jsonStr = reader.ReadToEnd();
            }
            paramsDictionary["city"] = "xyz";
            mockedHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage()
            {
                Content = new StringContent(jsonStr)
            })
            .Verifiable();

            //Action
            IActionResult actionaResultTask = await weatherForecastFunction?.GetWeatherForecastData(mockedHttpRequest?.Object);

            //Assert
            actionaResultTask.Should().As<BadRequestObjectResult>();
        }

        [TestMethod]
        public async Task TestGetWeatherForecastDataMethodForProperResponseWhenShouldIncludeTodayIsFalse()
        {
            //Arrange 
            String jsonStr = "";
            var streamData = Assembly.GetExecutingAssembly().GetManifestResourceStream("WeatherForecastTDD.ProperResponseFake.json");
            using (StreamReader reader = new StreamReader(streamData, Encoding.UTF8))
            {
                jsonStr = reader.ReadToEnd();
            }
            paramsDictionary["city"] = "kolkata";
            mockedHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage()
            {
                Content = new StringContent(jsonStr)
            })
            .Verifiable();

            //Action
            IActionResult actionaResultTask = await weatherForecastFunction?.GetWeatherForecastData(mockedHttpRequest?.Object);

            //Assert
            actionaResultTask.Should().As<OkObjectResult>();
        }

        [TestMethod]
        public async Task TestGetWeatherForecastDataMethodForProperResponseWhenShouldIncludeTodayIsTrue()
        {
            //Arrange 
            String jsonStr = "";
            var streamData = Assembly.GetExecutingAssembly().GetManifestResourceStream("WeatherForecastTDD.ProperResponseFake.json");
            using (StreamReader reader = new StreamReader(streamData, Encoding.UTF8))
            {
                jsonStr = reader.ReadToEnd();
            }
            paramsDictionary["city"] = "kolkata";
            paramsDictionary["shouldIncludeToday"] = "true";
            mockedHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage()
            {
                Content = new StringContent(jsonStr)
            })
            .Verifiable();

            //Action
            IActionResult actionaResultTask = await weatherForecastFunction?.GetWeatherForecastData(mockedHttpRequest?.Object);

            //Assert
            actionaResultTask.Should().As<OkObjectResult>();
        }

        [TestCleanup]
        public void CleanUp()
        {
            weatherForecastFunctionLoggerMock = null;
            weatherForecastFunction = null;
            validAuthKeyValue = null;
            mockedHttpRequest = null;
            paramsDictionary = null;
            mockedHttpMessageHandler = null;
            httpClient = null;
        }
    }
}
