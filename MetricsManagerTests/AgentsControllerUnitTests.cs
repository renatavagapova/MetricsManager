using MetricsManager;
using MetricsManager.Controllers;
using MetricsManager.DAL;
using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        [Fact]
        public void RegisterCheckRequestCreate()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<AgentsController>>();
            var mock = new Mock<IAgentsRepository>();
            mock.Setup(a => a.Create(new AgentModel())).Verifiable();
            var controller = new AgentsController(mock.Object, mockLogger.Object);
            //Act
            var result = controller.RegisterAgent(new AgentModel());
            //Assert
            mock.Verify(repository => repository.Create(new AgentModel()), Times.AtMostOnce());
            mockLogger.Verify();
        }

        [Fact]
        public void EnableAgentByIdCheckRequestCreate()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<AgentsController>>();
            var mockGet = new Mock<IAgentsRepository>();
            var mockUpdate = new Mock<IAgentsRepository>();
            mockGet.Setup(a => a.GetById(0)).Returns(new AgentModel()).Verifiable();
            mockUpdate.Setup(a => a.Update(new AgentModel())).Verifiable();
            var controller = new AgentsController(mockGet.Object, mockLogger.Object);
            //Act
            var result = controller.EnableAgentById(0);
            //Assert
            mockGet.Verify(repository => repository.GetById(0), Times.AtMostOnce());
            mockUpdate.Verify(repository => repository.Update(new AgentModel()), Times.AtMostOnce());
            mockLogger.Verify();
        }

        [Fact]
        public void DisableAgentByIdCheckRequestCreate()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<AgentsController>>();
            var mockGet = new Mock<IAgentsRepository>();
            var mockUpdate = new Mock<IAgentsRepository>();
            mockGet.Setup(a => a.GetById(0)).Returns(new AgentModel()).Verifiable();
            mockUpdate.Setup(a => a.Update(new AgentModel())).Verifiable();
            var controller = new AgentsController(mockGet.Object, mockLogger.Object);
            //Act
            var result = controller.DisableAgentById(0);
            //Assert
            mockGet.Verify(repository => repository.GetById(0), Times.AtMostOnce());
            mockUpdate.Verify(repository => repository.Update(new AgentModel()), Times.AtMostOnce());
            mockLogger.Verify();
        }

        [Fact]
        public void GetAllAgentsCheckRequestCreate()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<AgentsController>>();
            var mock = new Mock<IAgentsRepository>();
            mock.Setup(a => a.GetAll()).Returns(new List<AgentModel>()).Verifiable();
            var controller = new AgentsController(mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetAllAgents();
            //Assert
            mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
            mockLogger.Verify();
        }
    }
}
