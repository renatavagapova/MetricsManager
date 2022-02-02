using AutoMapper;
using MetricsLibrary;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _repository;
        private readonly IMapper _mapper;

        public RamMetricsController(IMapper mapper, IRamMetricsRepository repository, ILogger<RamMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех метрик Ram в заданном диапазоне времени для указанного агента
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/ram/agent/{idAgent}/from/{fromTime}/to/{toTime}
        ///
        /// </remarks>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <param name="agent">Id агента</param>
        /// <returns>Список метрик, которые были сохранены в репозитории и соответствуют заданному диапазону времени</returns>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("agent/{idAgent}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int idAgent,
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            var metrics = _repository.GetMetricsFromTimeToTimeFromAgent(fromTime, toTime, idAgent);
            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricManagerDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricManagerDto>(metric));
            }

            _logger.LogInformation($"Запрос метрик Ram FromTime = {fromTime} ToTime = {toTime} для агента Id = {idAgent}");

            return Ok(response);
        }

        /// <summary>
        /// Получение перцинтиля Ram в заданном диапазоне времени для указанного агента
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/ram/agent/{idAgent}/from/{fromTime}/to/{toTime}/percentiles/{percentile}
        ///
        /// </remarks>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <param name="agent">Id агента</param>
        /// <returns>Указанный  перцинтиль</returns>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("agent/{idAgent}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAgent(
            [FromRoute] int idAgent,
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime,
            [FromRoute] Percentile percentile)
        {
            var metrics = _repository.GetMetricsFromTimeToTimeFromAgentOrderBy(fromTime, toTime, "value", idAgent);
            if (metrics.Count == 0) return NoContent();

            int percentileThisList = (int)percentile;
            percentileThisList = percentileThisList * metrics.Count / 100;

            var response = metrics[percentileThisList].Value;

            _logger.LogInformation($"Запрос percentile Ram FromTime = {fromTime} ToTime = {toTime} percentile = {percentile} для агента Id = {idAgent}");

            return Ok(response);
        }

        /// <summary>
        /// Получение всех метрик Ram в заданном диапазоне времени для кластера
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/ram/cluster/from/{fromTime}/to/{toTime}
        ///
        /// </remarks>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <param name="agent">Id агента</param>
        /// <returns>Список метрик, которые были сохранены в репозитории и соответствуют заданному диапазону времени</returns>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromCluster(
                    [FromRoute] DateTimeOffset fromTime,
                    [FromRoute] DateTimeOffset toTime)
        {
            var metrics = _repository.GetMetricsFromTimeToTime(fromTime, toTime);
            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricManagerDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricManagerDto>(metric));
            }

            _logger.LogInformation($"Запрос метрик Ram FromTime = {fromTime} ToTime = {toTime} для кластера");

            return Ok(response);
        }

        /// <summary>
        /// Получение перцинтиля Ram в заданном диапазоне времени для всего кластера
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/ram/cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}
        /// 
        /// </remarks>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Указанный  перцинтиль</returns>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromCluster(
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime,
            [FromRoute] Percentile percentile)
        {
            var metrics = _repository.GetMetricsFromTimeToTimeOrderBy(fromTime, toTime, "value");
            if (metrics.Count == 0) return NoContent();

            int percentileThisList = (int)percentile;
            percentileThisList = percentileThisList * metrics.Count / 100;

            var response = metrics[percentileThisList].Value;

            _logger.LogInformation($"Запрос percentile = {percentile} Ram FromTime = {fromTime} ToTime = {toTime}");

            return Ok(response);
        }
    }
}
