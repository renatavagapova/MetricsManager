using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using System.Collections.Generic;
using MetricsAgent.Models;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

namespace MetricsAgentTests
{
    public class DotNetMetricsAgentControllerUnitTests
    {
        [Fact]
        public void GetDotNetMetricsFromTimeToTimeCheckRequestSelect()
        {
            //Arrange
            var mock = new Mock<IDotNetMetricsRepository>();
            var mockLogger = new Mock<ILogger<DotNetMetricsAgentController>>();
            TimeSpan fromTime = TimeSpan.FromSeconds(5);
            TimeSpan toTime = TimeSpan.FromSeconds(10);
            mock.Setup(a => a.GetMetricsFromTimeToTime(fromTime, toTime)).Returns(new List<DotNetMetric>()).Verifiable();
            var controller = new DotNetMetricsAgentController(mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsFromTimeToTime(fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTime(fromTime, toTime), Times.AtMostOnce());
            mockLogger.Verify();
        }
    }
}
