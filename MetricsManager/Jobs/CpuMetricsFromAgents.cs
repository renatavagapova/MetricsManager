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
    public class CpuMetricsFromAgents : IJob
    {
        private readonly ICpuMetricsRepository _repositoryCpu;
        private readonly IAgentsRepository _repositoryAgent;
        private readonly IMetricsManagerClient _client;

        public CpuMetricsFromAgents(ICpuMetricsRepository repositoryCpu, IAgentsRepository repositoryAgent, IMetricsManagerClient client)
        {
            _repositoryCpu = repositoryCpu;
            _repositoryAgent = repositoryAgent;
            _client = client;
        }

        public Task Execute(IJobExecutionContext context)
        {
            DateTimeOffset toTime = DateTimeOffset.UtcNow;
            DateTimeOffset fromTime = _repositoryCpu.LastTime();
            IList<AgentModel> agents = _repositoryAgent.GetAll();


            foreach (var agent in agents)
            {
                if (agent.Status == true)
                {
                    AllCpuMetricsApiResponse allCpuMetrics = _client.GetAllCpuMetrics(new GetAllCpuMetricsApiRequest
                    {
                        FromTime = fromTime,
                        ToTime = toTime,
                        Addres = agent.IpAddress
                    });

                    if (allCpuMetrics != null)
                    {
                        foreach (var metric in allCpuMetrics.Metrics)
                        {
                            _repositoryCpu.Create(new CpuMetricModel
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