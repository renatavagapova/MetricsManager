using AutoMapper;
using MetricsLibrary;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers
{
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly IAgentsRepository _repository;

        public AgentsController(IAgentsRepository repository, ILogger<AgentsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Регистрация нового агента сбора метрик
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/agents/register
        ///
        /// </remarks>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentModel agentInfo)
        {
            _repository.Create(agentInfo);

            _logger.LogInformation($"Регистрация агента: " +
                                   $"Id = {agentInfo.Id}" +
                                   $" IpAddress = {agentInfo.IpAddress}" +
                                   $" Name = {agentInfo.Name}" +
                                   $" Status = {agentInfo.Status}");

            return Ok();
        }

        /// <summary>
        /// Включение агента сбора метрик
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/agents/enable/{Idagent}
        ///
        /// </remarks>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpPut("enable/{Idagent}")]
        public IActionResult EnableAgentById([FromRoute] int Idagent)
        {
            AgentModel agent = _repository.GetById(Idagent);
            agent.Status = true;
            _repository.Update(agent);

            _logger.LogInformation($"Включение агента Id = {Idagent}");

            return Ok();
        }

        /// <summary>
        /// Выключение агента сбора метрик
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/agents/disable/{Idagent}
        ///
        /// </remarks>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpPut("disable/{Idagent}")]
        public IActionResult DisableAgentById([FromRoute] int Idagent)
        {
            AgentModel agent = _repository.GetById(Idagent);
            agent.Status = false;
            _repository.Update(agent);

            _logger.LogInformation($"Отключение агента Id = {Idagent}");

            return Ok();
        }

        /// <summary>
        /// Получение всех зарегистрированных агентов
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/agents/all
        ///
        /// </remarks>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("all")]
        public IActionResult GetAllAgents()
        {
            var metrics = _repository.GetAll();
            var response = new AllAgentsResponse()
            {
                Metrics = new List<AgentManagerDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new AgentManagerDto
                {
                    Id = metric.Id,
                    Status = metric.Status,
                    IpAddress = metric.IpAddress,
                    Name = metric.Name
                });
            }

            _logger.LogInformation("Запрос всех агентов");


            return Ok(response);
        }
    }
}