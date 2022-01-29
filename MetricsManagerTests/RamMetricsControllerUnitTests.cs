using MetricsManager.Controllers;
using System.Collections.Generic;
using MetricsLibrary;
using System;
using AutoFixture;
using AutoMapper;
using Xunit;
using Moq;
using MetricsManager.DAL;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManagerTests
{
    public class RamMetricsControllerUnitTests
    {
        private readonly Mock<IRamMetricsRepository> _repository;
        private readonly RamMetricsController _controller;
        private readonly Mock<ILogger<RamMetricsController>> _logger;
        private readonly IMapper _mapper;

        public RamMetricsControllerUnitTests()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RamMetricModel, RamMetricManagerDto>());
            _mapper = config.CreateMapper();
            _logger = new Mock<ILogger<RamMetricsController>>();
            _repository = new Mock<IRamMetricsRepository>();
            _controller = new RamMetricsController(_mapper, _repository.Object, _logger.Object);
        }

        [Fact]
        public void GetRamMetricsFromAgent()
        {
            //Arrange
            var fixture = new Fixture();
            var returnList = fixture.Create<List<RamMetricModel>>();

            _repository.Setup(a => a.GetMetricsFromTimeToTimeFromAgent(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), 1))
                .Returns(returnList).Verifiable();
            //Act
            var result = (OkObjectResult)_controller.GetMetricsFromAgent(1,
                DateTimeOffset.FromUnixTimeSeconds(0),
                DateTimeOffset.FromUnixTimeSeconds(17000000000));
            var actualResult = (AllRamMetricsResponse)result.Value;
            //Assert
            _repository.Verify(repository => repository.GetMetricsFromTimeToTimeFromAgent(
                DateTimeOffset.FromUnixTimeSeconds(0),
                DateTimeOffset.FromUnixTimeSeconds(17000000000),
                1),
                Times.Once());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
            for (int i = 0; i < returnList.Count; i++)
            {
                Assert.Equal(returnList[i].Id, actualResult.Metrics[i].Id);
                Assert.Equal(returnList[i].Value, actualResult.Metrics[i].Value);
                Assert.Equal(returnList[i].IdAgent, actualResult.Metrics[i].IdAgent);
            }
            _logger.Verify();
        }

        [Fact]
        public void GetRamMetricsFromCluster()
        {
            //Arrange
            var fixture = new Fixture();
            var returnList = fixture.Create<List<RamMetricModel>>();

            _repository.Setup(a => a.GetMetricsFromTimeToTime(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(returnList).Verifiable();
            //Act
            var result = (OkObjectResult)_controller.GetMetricsFromCluster(
                DateTimeOffset.FromUnixTimeSeconds(0),
                DateTimeOffset.FromUnixTimeSeconds(17000000000));
            var actualResult = (AllRamMetricsResponse)result.Value;
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
                Assert.Equal(returnList[i].IdAgent, actualResult.Metrics[i].IdAgent);
            }
            _logger.Verify();
        }

        [Fact]
        public void GetRamPercentileFromAgent()
        {
            //Arrange
            var fixture = new Fixture();
            var returnList = fixture.Create<List<RamMetricModel>>();

            _repository.Setup(a => a.GetMetricsFromTimeToTimeFromAgentOrderBy(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), "value", 1))
                .Returns(returnList).Verifiable();
            //Act
            var result = (OkObjectResult)_controller.GetMetricsByPercentileFromAgent(1,
                DateTimeOffset.FromUnixTimeSeconds(0),
                DateTimeOffset.FromUnixTimeSeconds(17000000000),
                Percentile.P90);
            //Assert
            _repository.Verify(repository => repository.GetMetricsFromTimeToTimeFromAgentOrderBy(
                DateTimeOffset.FromUnixTimeSeconds(0),
                DateTimeOffset.FromUnixTimeSeconds(17000000000),
                "value",
                1),
                Times.Once());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
            int percentileThisList = (int)Percentile.P90;
            percentileThisList = percentileThisList * returnList.Count / 100;

            var returnPercentile = returnList[percentileThisList].Value;
            Assert.Equal(returnPercentile, result.Value);
            _logger.Verify();
        }

        [Fact]
        public void GetRamPercentileFromCluster()
        {
            //Arrange
            var fixture = new Fixture();
            var returnList = fixture.Create<List<RamMetricModel>>();

            _repository.Setup(a => a.GetMetricsFromTimeToTimeOrderBy(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), "value"))
                .Returns(returnList).Verifiable();
            //Act
            var result = (OkObjectResult)_controller.GetMetricsByPercentileFromCluster(
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