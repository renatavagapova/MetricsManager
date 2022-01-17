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
            TimeSpan fromTime = TimeSpan.FromSeconds(5);
            TimeSpan toTime = TimeSpan.FromSeconds(10);
            mock.Setup(a => a.GetMetricsFromTimeToTime(fromTime, toTime)).Returns(new List<NetworkMetric>()).Verifiable();
            var controller = new NetworkMetricsAgentController(mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsNetwork(fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTime(fromTime, toTime), Times.AtMostOnce());
            mockLogger.Verify();
        }
    }
}
