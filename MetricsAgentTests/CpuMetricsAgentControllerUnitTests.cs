using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using MetricsAgent.Responses;
using MetricsLibrary;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuMetricsAgentControllerUnitTests
    {
        [Fact]
        public void GetCpuMetricsFromTimeToTimeCheckRequestSelect()
        {
            //Arrange
            var mock = new Mock<ICpuMetricsRepository>();
            var mockLogger = new Mock<ILogger<CpuMetricsAgentController>>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CpuMetricModel, CpuMetricDto>());
            IMapper mapper = config.CreateMapper();
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(5);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(10);
            mock.Setup(a => a.GetMetricsFromTimeToTime(fromTime, toTime)).Returns(new List<CpuMetricModel>()).Verifiable();
            var controller = new CpuMetricsAgentController(mapper, mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsFromTimeToTime(fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTime(fromTime, toTime), Times.AtMostOnce());
            mockLogger.Verify();
        }

        [Fact]
        public void GetMetricsFromTimeToTimeByPercentileCheckRequestSelect()
        {
            //Arrange
            var mock = new Mock<ICpuMetricsRepository>();
            var mockLogger = new Mock<ILogger<CpuMetricsAgentController>>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CpuMetricModel, CpuMetricDto>());
            IMapper mapper = config.CreateMapper();
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(5);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(10);
            Percentile percentile = Percentile.P90;
            string sort = "value";
            mock.Setup(a => a.GetMetricsFromTimeToTimeOrderBy(fromTime, toTime, sort)).Returns(new List<CpuMetricModel>()).Verifiable();
            var controller = new CpuMetricsAgentController(mapper, mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsFromTimeToTimePercentile(fromTime, toTime, percentile);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTime(fromTime, toTime), Times.AtMostOnce());
            mockLogger.Verify();
        }
    }
}
