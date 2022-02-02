using MetricsManager.Client;
using MetricsManager.Client.ApiRequests;
using MetricsManager.Client.ApiResponses;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class HddMetricsFromAgents : IJob
    {
        private readonly IHddMetricsRepository _repositoryHdd;
        private readonly IAgentsRepository _repositoryAgent;
        private readonly IMetricsManagerClient _client;

        public HddMetricsFromAgents(IHddMetricsRepository repositoryHdd, IAgentsRepository repositoryAgent, IMetricsManagerClient client)
        {
            _repositoryHdd = repositoryHdd;
            _repositoryAgent = repositoryAgent;
            _client = client;
        }

        public Task Execute(IJobExecutionContext context)
        {
            DateTimeOffset toTime = DateTimeOffset.UtcNow;
            DateTimeOffset fromTime = _repositoryHdd.LastTime();
            IList<AgentModel> agents = _repositoryAgent.GetAll();


            foreach (var agent in agents)
            {
                if (agent.Status == true)
                {
                    AllHddMetricsApiResponse allHddMetrics = _client.GetAllHddMetrics(new GetAllHddMetricsApiRequest
                    {
                        FromTime = fromTime,
                        ToTime = toTime,
                        Addres = agent.IpAddress
                    });

                    if (allHddMetrics != null)
                    {
                        foreach (var metric in allHddMetrics.Metrics)
                        {
                            _repositoryHdd.Create(new HddMetricModel
                            {
                                IdAgent = agent.Id,
                                Time = metric.Time,
                                Value = metric.Value
                            });
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}