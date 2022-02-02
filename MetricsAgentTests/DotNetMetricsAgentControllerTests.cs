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
using Microsoft.AspNetCore.Mvc;
using AutoFixture;

namespace MetricsAgentTests
{
    public class DotNetMetricsAgentControllerTests
    {
        private readonly Mock<IDotNetMetricsRepository> _repository;
        private readonly DotNetMetricsAgentController _controller;
        private readonly Mock<ILogger<DotNetMetricsAgentController>> _logger;
        private readonly IMapper _mapper;

        public DotNetMetricsAgentControllerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DotNetMetricModel, DotNetMetricDto>());
            _mapper = config.CreateMapper();
            _logger = new Mock<ILogger<DotNetMetricsAgentController>>();
            _repository = new Mock<IDotNetMetricsRepository>();
            _controller = new DotNetMetricsAgentController(_mapper, _repository.Object, _logger.Object);
        }

        [Fact]
        public void GetDotNetMetricsFromTimeToTime()
        {
            //Arrange
            var fixture = new Fixture();
            var returnList = fixture.Create<List<DotNetMetricModel>>();

            _repository.Setup(a => a.GetMetricsFromTimeToTime(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                .Returns(returnList).Verifiable();
            //Act
            var result = (OkObjectResult)_controller.GetMetricsFromTimeToTime(
                DateTimeOffset.FromUnixTimeSeconds(0),
                DateTimeOffset.FromUnixTimeSeconds(17000000000));
            var actualResult = (AllDotNetMetricsResponse)result.Value;
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
