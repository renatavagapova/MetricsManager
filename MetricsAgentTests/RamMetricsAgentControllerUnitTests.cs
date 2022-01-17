using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMetricsAgentControllerUnitTests
    {
        [Fact]
        public void GetMetricsAvailableCheckRequestSelect()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<RamMetricsAgentController>>();
            var mock = new Mock<IRamMetricsRepository>();
            mock.Setup(a => a.GetAll()).Returns(new List<RamMetric>()).Verifiable();
            var controller = new RamMetricsAgentController(mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsAvailableRam();
            //Assert
            mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
            mockLogger.Verify();
        }
    }
}
