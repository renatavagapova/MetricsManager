using AutoFixture;
using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using MetricsAgent.Responses;
using MetricsLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuMetricsAgentControllerTests
    {
        private readonly Mock<ICpuMetricsRepository> _repository;
        private readonly CpuMetricsAgentController _controller;
        private readonly Mock<ILogger<CpuMetricsAgentController>> _logger;
        private readonly IMapper _mapper;

        public CpuMetricsAgentControllerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CpuMetricModel, CpuMetricDto>());
            _mapper = config.CreateMapper();
            _logger = new Mock<ILogger<CpuMetricsAgentController>>();
            _repository = new Mock<ICpuMetricsRepository>();
            _controller = new CpuMetricsAgentController(_mapper, _repository.Object, _logger.Object);
        }

        [Fact]
        public void GetCpuMetricsFromTimeToTime()
        {
            //Arrange
            var fixture = new Fixture();
            var returnList = fixture.Create<List<CpuMetricModel>>();

            _repository.Setup(a => a.GetMetricsFromTimeToTime(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(returnList).Verifiable();
            //Act
            var result = (OkObjectResult)_controller.GetMetricsFromTimeToTime(
                DateTimeOffset.FromUnixTimeSeconds(0),
                DateTimeOffset.FromUnixTimeSeconds(17000000000));
            var actualResult = (AllCpuMetricsResponse)result.Value;
            //Assert
            _repository.Verify(repository => repository.GetMetricsFromTimeToTime(
                    DateTimeOffset.FromUnixTimeSeconds(0),
                    DateTimeOffset.FromUnixTimeSeconds(17000000000)),
                Times.Once());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
            for (int i = 0; i < returnList.Count; i++)
            {
                Assert.Equal(returnList[i].Id, actualResult.Metrics[i].Id);
                Assert.Equal(returnList[i].Value, actualResult.Metrics[i].Value);
            }
            _logger.Verify();
        }

        [Fact]
        public void GetMetricsFromTimeToTimeByPercentile()
        {
            //Arrange
            var fixture = new Fixture();
            var returnList = fixture.Create<List<CpuMetricModel>>();

            _repository.Setup(a => a.GetMetricsFromTimeToTimeOrderBy(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), "value"))
                .Returns(returnList).Verifiable();
            //Act
            var result = (OkObjectResult)_controller.GetMetricsFromTimeToTimePercentile(
                DateTimeOffset.FromUnixTimeSeconds(0),
                DateTimeOffset.FromUnixTimeSeconds(17000000000),
                Percentile.P90);
            //Assert
            _repository.Verify(repository => repository.GetMetricsFromTimeToTimeOrderBy(
                    DateTimeOffset.FromUnixTimeSeconds(0),
                    DateTimeOffset.FromUnixTimeSeconds(17000000000),
                    "value"),
                Times.Once());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
            int percentileThisList = (int)Percentile.P90;
            percentileThisList = percentileThisList * returnList.Count / 100;

            var returnPercentile = returnList[percentileThisList].Value;
            Assert.Equal(returnPercentile, result.Value);
            _logger.Verify();
        }
    }
}
