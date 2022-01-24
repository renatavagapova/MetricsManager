using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using MetricsAgent;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using System.Collections.Generic;
using MetricsAgent.DAL.Interfaces;
using AutoMapper;
using MetricsAgent.Responses;

namespace MetricsAgentTests
{
    public class NetworkMetricsAgentControllerUnitTests
    {
        [Fact]
        public void GetMetricsNetworkCheckRequestSelect()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<NetworkMetricsAgentController>>();
            var mock = new Mock<INetworkMetricsRepository>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NetworkMetricModel, NetworkMetricDto>());
            IMapper mapper = config.CreateMapper();
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(5);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(10);
            mock.Setup(a => a.GetMetricsFromTimeToTime(fromTime, toTime)).Returns(new List<NetworkMetricModel>()).Verifiable();
            var controller = new NetworkMetricsAgentController(mapper, mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsNetwork(fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTime(fromTime, toTime), Times.AtMostOnce());
            mockLogger.Verify();
        }
    }
}
