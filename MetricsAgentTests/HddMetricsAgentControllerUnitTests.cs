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

namespace MetricsAgentTests
{
    public class HddMetricsAgentControllerUnitTests
    {
        [Fact]
        public void GetMetricsFreeHddCheckRequestSelect()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<HddMetricsAgentController>>();
            var mock = new Mock<IHddMetricsRepository>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<HddMetricModel, HddMetricDto>());
            IMapper mapper = config.CreateMapper();
            mock.Setup(a => a.GetAll()).Returns(new List<HddMetricModel>()).Verifiable();
            var controller = new HddMetricsAgentController(mapper, mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsFreeHdd();
            //Assert
            mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
            mockLogger.Verify();
        }
    }
}
