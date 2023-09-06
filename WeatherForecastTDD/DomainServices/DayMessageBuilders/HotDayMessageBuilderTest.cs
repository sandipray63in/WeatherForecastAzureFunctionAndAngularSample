using Azure.Security.KeyVault.Secrets;
using FluentAssertions;
using Moq;
using WeatherForecastAPI.Domain.WeatherForecastResponse;
using WeatherForecastAPI.DomainServices;
using WeatherForecastAPI.DomainServices.DayMessageBuilders;

namespace WeatherForecastTDD.DomainServices.DayMessageBuilders
{
    [TestClass]
    public class HotDayMessageBuilderTest
    {
        [TestMethod]
        public void TestGetAllMessagesMethod()
        {
            //Arrange
            var mockedSecretClient = new Mock<SecretClient>();
            IList<List> responseDataList = new List<List>();
            List list = new List();
            list.dt_txt = "2023-09-01 09:00:00";
            list.main = new Main();
            list.main.temp = 113;// temp in farenheit so that temp in celcius is 45 which > 40
            responseDataList.Add(list);
            mockedSecretClient.Setup(x => x.GetSecret("maxHotDayTempLimit",null,default).Value).Returns(new KeyVaultSecret("maxHotDayTempLimit","40"));
            HotDayMessageBuilder.SetSecretClient(mockedSecretClient.Object);
            IDayMessageBuilder dayMessageBuilder = new HotDayMessageBuilder();

            //Action
            IList<string> allMessages = dayMessageBuilder.GetAllMessages(responseDataList);

            //Assert
            allMessages.Should().OnlyContain(x => x.Contains("Use sunscreen lotion"));
        }
    }
}
