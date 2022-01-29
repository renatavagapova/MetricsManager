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
    public class DotNetMetricsFromAgents : IJob
    {
        private readonly IDotNetMetricsRepository _repositoryDotNet;
        private readonly IAgentsRepository _repositoryAgent;
        private readonly IMetricsManagerClient _client;

        public DotNetMetricsFromAgents(IDotNetMetricsRepository repositoryDotNet, IAgentsRepository repositoryAgent, IMetricsManagerClient client)
        {
            _repositoryDotNet = repositoryDotNet;
            _repositoryAgent = repositoryAgent;
            _client = client;
        }

        public Task Execute(IJobExecutionContext context)
        {
            DateTimeOffset toTime = DateTimeOffset.UtcNow;
            DateTimeOffset fromTime = _repositoryDotNet.LastTime();
            IList<AgentModel> agents = _repositoryAgent.GetAll();


            foreach (var agent in agents)
            {
                if (agent.Status == true)
                {
                    AllDotNetMetricsApiResponse allDotNetMetrics = _client.GetAllDotNetMetrics(new GetAllDotNetMetricsApiRequest
                    {
                        FromTime = fromTime,
                        ToTime = toTime,
                        Addres = agent.IpAddress
                    });

                    if (allDotNetMetrics != null)
                    {
                        foreach (var metric in allDotNetMetrics.Metrics)
                        {
                            _repositoryDotNet.Create(new DotNetMetricModel
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