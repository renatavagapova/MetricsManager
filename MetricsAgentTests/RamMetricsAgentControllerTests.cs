using AutoFixture;
using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMetricsAgentControllerTests
    {
        private readonly Mock<IRamMetricsRepository> _repository;
        private readonly RamMetricsAgentController _controller;
        private readonly Mock<ILogger<RamMetricsAgentController>> _logger;
        private readonly IMapper _mapper;

        public RamMetricsAgentControllerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RamMetricModel, RamMetricDto>());
            _mapper = config.CreateMapper();
            _logger = new Mock<ILogger<RamMetricsAgentController>>();
            _repository = new Mock<IRamMetricsRepository>();
            _controller = new RamMetricsAgentController(_mapper, _repository.Object, _logger.Object);
        }

        [Fact]
        public void GetRamMetricsFromTimeToTime()
        {
            //Arrange
            var fixture = new Fixture();
            var returnList = fixture.Create<List<RamMetricModel>>();

            _repository.Setup(a => a.GetMetricsFromTimeToTime(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(returnList).Verifiable();
            //Act
            var result = (OkObjectResult)_controller.GetMetricsFromTimeToTime(
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
                Assert.Equal(returnList[i].Available, actualResult.Metrics[i].Available);
            }
            _logger.Verify();
        }
    }
}
