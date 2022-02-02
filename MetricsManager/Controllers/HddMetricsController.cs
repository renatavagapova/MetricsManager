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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHddMetricsRepository _repository;
        private readonly IMapper _mapper;

        public HddMetricsController(IMapper mapper, IHddMetricsRepository repository, ILogger<HddMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех метрик Hdd в заданном диапазоне времени для указанного агента
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/hdd/agent/{idAgent}/from/{fromTime}/to/{toTime}
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
            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricManagerDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricManagerDto>(metric));
            }

            _logger.LogInformation($"Запрос метрик Hdd FromTime = {fromTime} ToTime = {toTime} для агента Id = {idAgent}");

            return Ok(response);
        }

        /// <summary>
        /// Получение перцинтиля Hdd в заданном диапазоне времени для указанного агента
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/hdd/agent/{idAgent}/from/{fromTime}/to/{toTime}/percentiles/{percentile}
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

            _logger.LogInformation($"Запрос percentile Hdd FromTime = {fromTime} ToTime = {toTime} percentile = {percentile} для агента Id = {idAgent}");

            return Ok(response);
        }

        /// <summary>
        /// Получение всех метрик Hdd в заданном диапазоне времени для кластера
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/hdd/cluster/from/{fromTime}/to/{toTime}
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
            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricManagerDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricManagerDto>(metric));
            }

            _logger.LogInformation($"Запрос метрик Hdd FromTime = {fromTime} ToTime = {toTime} для кластера");

            return Ok(response);
        }

        /// <summary>
        /// Получение перцинтиля Hdd в заданном диапазоне времени для всего кластера
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// GET api/metrics/hdd/cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}
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

            _logger.LogInformation($"Запрос percentile = {percentile} Hdd FromTime = {fromTime} ToTime = {toTime}");

            return Ok(response);
        }
    }
}
