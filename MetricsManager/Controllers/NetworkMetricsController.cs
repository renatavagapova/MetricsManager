using MetricsLibrary;
using MetricsManager.DAL;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
  
        [Route("api/metrics/network")]
        [ApiController]
        public class NetworkMetricsController : ControllerBase
        {
            private readonly ILogger<NetworkMetricsController> _logger;
            private INetworkMetricsRepository _repository;

            public NetworkMetricsController(INetworkMetricsRepository repository, ILogger<NetworkMetricsController> logger)
            {
                _repository = repository;
                _logger = logger;
            }

            [HttpGet("agent/{idAgent}/from/{fromTime}/to/{toTime}")]
            public IActionResult GetMetricsFromAgent(
                [FromRoute] int idAgent,
                [FromRoute] TimeSpan fromTime,
                [FromRoute] TimeSpan toTime)
            {
                var metrics = _repository.GetMetricsFromTimeToTimeFromAgent(fromTime, toTime, idAgent);
                var response = new AllNetworkMetricsResponse()
                {
                    Metrics = new List<NetworkMetricManagerDto>()
                };

                foreach (var metric in metrics)
                {
                    response.Metrics.Add(new NetworkMetricManagerDto
                    {
                        Time = metric.Time,
                        Value = metric.Value,
                        Id = metric.Id,
                        IdAgent = metric.IdAgent
                    });
                }

                if (_logger != null)
                {
                    _logger.LogInformation("Запрос метрик Network FromTimeToTime для агента");
                }

                return Ok(response);
            }

            [HttpGet("agent/{idAgent}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
            public IActionResult GetMetricsByPercentileFromAgent(
                [FromRoute] int idAgent,
                [FromRoute] TimeSpan fromTime,
                [FromRoute] TimeSpan toTime,
                [FromRoute] Percentile percentile)
            {
                var metrics = _repository.GetMetricsFromTimeToTimeFromAgentOrderBy(fromTime, toTime, "value", idAgent);
                if (metrics.Count == 0) return NoContent();

                int percentileThisList = (int)percentile;
                percentileThisList = percentileThisList * metrics.Count / 100;

                var response = new AllNetworkMetricsResponse()
                {
                    Metrics = new List<NetworkMetricManagerDto>()
                };

                response.Metrics.Add(new NetworkMetricManagerDto
                {
                    Time = metrics[percentileThisList].Time,
                    Value = metrics[percentileThisList].Value,
                    Id = metrics[percentileThisList].Id,
                    IdAgent = metrics[percentileThisList].IdAgent
                });

                if (_logger != null)
                {
                    _logger.LogInformation("Запрос percentile Network FromTimeToTime");
                }

                return Ok(response);
            }

            [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
            public IActionResult GetMetricsFromAllCluster(
                        [FromRoute] TimeSpan fromTime,
                        [FromRoute] TimeSpan toTime)
            {
                var metrics = _repository.GetMetricsFromTimeToTime(fromTime, toTime);
                var response = new AllNetworkMetricsResponse()
                {
                    Metrics = new List<NetworkMetricManagerDto>()
                };

                foreach (var metric in metrics)
                {
                    response.Metrics.Add(new NetworkMetricManagerDto
                    {
                        Time = metric.Time,
                        Value = metric.Value,
                        Id = metric.Id,
                        IdAgent = metric.IdAgent
                    });
                }

                if (_logger != null)
                {
                    _logger.LogInformation("Запрос метрик Network FromTimeToTime для кластера");
                }

                return Ok(response);
            }

            [HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
            public IActionResult GetMetricsByPercentileFromCluster(
                [FromRoute] TimeSpan fromTime,
                [FromRoute] TimeSpan toTime,
                [FromRoute] Percentile percentile)
            {
                var metrics = _repository.GetMetricsFromTimeToTimeOrderBy(fromTime, toTime, "value");
                if (metrics.Count == 0) return NoContent();

                int percentileThisList = (int)percentile;
                percentileThisList = percentileThisList * metrics.Count / 100;

                var response = new AllNetworkMetricsResponse()
                {
                    Metrics = new List<NetworkMetricManagerDto>()
                };

                response.Metrics.Add(new NetworkMetricManagerDto
                {
                    Time = metrics[percentileThisList].Time,
                    Value = metrics[percentileThisList].Value,
                    Id = metrics[percentileThisList].Id,
                    IdAgent = metrics[percentileThisList].IdAgent
                });

                if (_logger != null)
                {
                    _logger.LogInformation("Запрос percentile Network FromTimeToTime");
                }

                return Ok(response);
            }
        }
    
}