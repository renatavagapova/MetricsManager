using MetricsAgent.Controllers;
using System.Collections.Generic;
using MetricsAgent.Models;
using Xunit;
using Moq;
using MetricsAgent.DAL;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Responses;
using AutoFixture;
using System;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class HddMetricsAgentControllerTests
    {
        private readonly Mock<IHddMetricsRepository> _repository;
        private readonly HddMetricsAgentController _controller;
        private readonly Mock<ILogger<HddMetricsAgentController>> _logger;
        private readonly IMapper _mapper;

        public HddMetricsAgentControllerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<HddMetricModel, HddMetricDto>());
            _mapper = config.CreateMapper();
            _logger = new Mock<ILogger<HddMetricsAgentController>>();
            _repository = new Mock<IHddMetricsRepository>();
            _controller = new HddMetricsAgentController(_mapper, _repository.Object, _logger.Object);
        }

        [Fact]
        public void GetHddMetricsFromTimeToTime()
        {
            //Arrange
            var fixture = new Fixture();
            var returnList = fixture.Create<List<HddMetricModel>>();

            _repository.Setup(a => a.GetMetricsFromTimeToTime(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(returnList).Verifiable();
            //Act
            var result = (OkObjectResult)_controller.GetMetricsFromTimeToTime(
                DateTimeOffset.FromUnixTimeSeconds(0),
                DateTimeOffset.FromUnixTimeSeconds(17000000000));
            var actualResult = (AllHddMetricsResponse)result.Value;
            //Assert
            _repository.Verify(repository => repository.GetMetricsFromTimeToTime(
                    DateTimeOffset.FromUnixTimeSeconds(0),
                    DateTimeOffset.FromUnixTimeSeconds(17000000000)),
                Times.Once());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
            for (int i = 0; i < returnList.Count; i++)
            {
                Assert.Equal(returnList[i].Id, actualResult.Metrics[i].Id);
                Assert.Equal(returnList[i].FreeSize, actualResult.Metrics[i].FreeSize);
            }
            _logger.Verify();
        }
    }
}
