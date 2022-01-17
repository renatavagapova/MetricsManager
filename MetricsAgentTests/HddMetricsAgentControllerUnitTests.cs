using MetricsAgent.Controllers;
using System.Collections.Generic;
using MetricsAgent.Models;
using Xunit;
using Moq;
using MetricsAgent.DAL;
using Microsoft.Extensions.Logging;

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
            mock.Setup(a => a.GetAll()).Returns(new List<HddMetric>()).Verifiable();
            var controller = new HddMetricsAgentController(mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsFreeHdd();
            //Assert
            mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
            mockLogger.Verify();
        }
    }
}
