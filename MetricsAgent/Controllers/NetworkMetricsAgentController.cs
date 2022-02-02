using AutoMapper;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsAgentController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsAgentController> _logger;
        private readonly INetworkMetricsRepository _repository;
        private readonly IMapper _mapper;

        public NetworkMetricsAgentController(IMapper mapper, INetworkMetricsRepository repository, ILogger<NetworkMetricsAgentController> logger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех метрик Network в заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/network/from/{fromTime}/to/{toTime}
        ///
        /// </remarks>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Список метрик, которые были сохранены в репозитории и соответствуют заданному диапазону времени</returns>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromTimeToTime(
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            IList<NetworkMetricModel> metrics = _repository.GetMetricsFromTimeToTime(fromTime, toTime);
            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }

            _logger.LogInformation($"Запрос метрик Network FromTime = {fromTime} ToTime = {toTime}");

            return Ok(response);
        }
    }
}
