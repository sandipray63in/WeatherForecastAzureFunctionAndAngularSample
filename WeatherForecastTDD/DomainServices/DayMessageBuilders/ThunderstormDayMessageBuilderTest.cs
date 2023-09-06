﻿using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastAPI.Domain.WeatherForecastResponse;
using WeatherForecastAPI.DomainServices.DayMessageBuilders;
using WeatherForecastAPI.DomainServices;

namespace WeatherForecastTDD.DomainServices.DayMessageBuilders
{
    [TestClass]
    public class ThunderstormDayMessageBuilderTest
    {
        [TestMethod]
        public void TestGetAllMessagesMethod()
        {
            //Arrange
            IList<List> responseDataList = new List<List>();
            List list = new List();
            list.dt_txt = "2023-09-01 09:00:00";
            list.weather = new List<Weather> { new Weather() { main="thunder" } };
            responseDataList.Add(list);
            IDayMessageBuilder dayMessageBuilder = new ThunderstormDayMessageBuilder();

            //Action
            IList<string> allMessages = dayMessageBuilder.GetAllMessages(responseDataList);

            //Assert
            allMessages.Should().OnlyContain(x => x.Contains("Don’t step out! A Storm is brewing!"));
        }
    }
}
