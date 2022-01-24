using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using System.Collections.Generic;
using MetricsAgent.Models;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL.Interfaces;
using AutoMapper;
using MetricsAgent.Responses;

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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DotNetMetricModel, DotNetMetricDto>());
            IMapper mapper = config.CreateMapper();
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(5);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(10);
            mock.Setup(a => a.GetMetricsFromTimeToTime(fromTime, toTime)).Returns(new List<DotNetMetricModel>()).Verifiable();
            var controller = new DotNetMetricsAgentController(mapper, mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsFromTimeToTime(fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTime(fromTime, toTime), Times.AtMostOnce());
            mockLogger.Verify();
        }
    }
}
