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
using AutoFixture;

namespace MetricsAgentTests
{
    public class NetworkMetricsAgentControllerTests
    {
        private readonly Mock<INetworkMetricsRepository> _repository;
        private readonly NetworkMetricsAgentController _controller;
        private readonly Mock<ILogger<NetworkMetricsAgentController>> _logger;
        private readonly IMapper _mapper;

        public NetworkMetricsAgentControllerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NetworkMetricModel, NetworkMetricDto>());
            _mapper = config.CreateMapper();
            _logger = new Mock<ILogger<NetworkMetricsAgentController>>();
            _repository = new Mock<INetworkMetricsRepository>();
            _controller = new NetworkMetricsAgentController(_mapper, _repository.Object, _logger.Object);
        }

        [Fact]
        public void GetNetworkMetricsFromTimeToTime()
        {
            //Arrange
            var fixture = new Fixture();
            var returnList = fixture.Create<List<NetworkMetricModel>>();

            _repository.Setup(a => a.GetMetricsFromTimeToTime(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(returnList).Verifiable();
            //Act
            var result = (OkObjectResult)_controller.GetMetricsFromTimeToTime(
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
            }
            _logger.Verify();
        }
    }
}
