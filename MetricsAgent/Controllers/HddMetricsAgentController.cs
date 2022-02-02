using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsAgentController : ControllerBase
    {
        private readonly ILogger<HddMetricsAgentController> _logger;
        private readonly IHddMetricsRepository _repository;
        private readonly IMapper _mapper;

        public HddMetricsAgentController(IMapper mapper, IHddMetricsRepository repository, ILogger<HddMetricsAgentController> logger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех метрик Hdd
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/hdd/all
        ///
        /// </remarks>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("all")]
        public IActionResult GetMetricsAvailableHdd()
        {
            IList<HddMetricModel> metrics = _repository.GetAll();
            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }

            _logger.LogInformation("Запрос всех метрик Available Hdd");

            return Ok(response);
        }

        /// <summary>
        /// Получение всех метрик Hdd в заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/hdd/from/{fromTime}/to/{toTime}
        ///
        /// </remarks>
        /// <paHdd name="fromTime">начальная метка времени в формате DateTimeOffset</paHdd>
        /// <paHdd name="toTime">конечная метка времени в формате DateTimeOffset</paHdd>
        /// <returns>Список метрик, которые были сохранены в репозитории и соответствуют заданному диапазону времени</returns>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromTimeToTime(
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            IList<HddMetricModel> metrics = _repository.GetMetricsFromTimeToTime(fromTime, toTime);
            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }

            _logger.LogInformation($"Запрос метрик Hdd FromTime = {fromTime} ToTime = {toTime}");

            return Ok(response);
        }
    }
}
