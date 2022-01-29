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
    public class NetworkMetricsControllerUnitTests
    {
        private readonly Mock<INetworkMetricsRepository> _repository;
        private readonly NetworkMetricsController _controller;
        private readonly Mock<ILogger<NetworkMetricsController>> _logger;
        private readonly IMapper _mapper;

        public NetworkMetricsControllerUnitTests()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NetworkMetricModel, NetworkMetricManagerDto>());
            _mapper = config.CreateMapper();
            _logger = new Mock<ILogger<NetworkMetricsController>>();
            _repository = new Mock<INetworkMetricsRepository>();
            _controller = new NetworkMetricsController(_mapper, _repository.Object, _logger.Object);
        }

        [Fact]
        public void GetNetworkMetricsFromAgent()
        {
            //Arrange
            var fixture = new Fixture();
            var returnList = fixture.Create<List<NetworkMetricModel>>();

            _repository.Setup(a => a.GetMetricsFromTimeToTimeFromAgent(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), 1))
                .Returns(returnList).Verifiable();
            //Act
            var result = (OkObjectResult)_controller.GetMetricsFromAgent(1,
                DateTimeOffset.FromUnixTimeSeconds(0),
                DateTimeOffset.FromUnixTimeSeconds(17000000000));
            var actualResult = (AllNetworkMetricsResponse)result.Value;
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
        public void GetNetworkMetricsFromCluster()
        {
            //Arrange
            var fixture = new Fixture();
            var returnList = fixture.Create<List<NetworkMetricModel>>();

            _repository.Setup(a => a.GetMetricsFromTimeToTime(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(returnList).Verifiable();
            //Act
            var result = (OkObjectResult)_controller.GetMetricsFromCluster(
                DateTimeOffset.FromUnixTimeSeconds(0),
                DateTimeOffset.FromUnixTimeSeconds(17000000000));
            var actualResult = (AllNetworkMetricsResponse)result.Value;
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
        public void GetNetworkPercentileFromAgent()
        {
            //Arrange
            var fixture = new Fixture();
            var returnList = fixture.Create<List<NetworkMetricModel>>();

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
        public void GetNetworkPercentileFromCluster()
        {
            //Arrange
            var fixture = new Fixture();
            var returnList = fixture.Create<List<NetworkMetricModel>>();

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