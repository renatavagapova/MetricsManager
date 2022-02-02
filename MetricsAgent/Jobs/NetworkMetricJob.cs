using MetricsAgent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.Diagnostics;
using MetricsAgent.Models;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class NetworkMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly INetworkMetricsRepository _repository;
        private readonly PerformanceCounter _networkCounter;

        public NetworkMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<INetworkMetricsRepository>();
            _networkCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", "Intel[R] Wireless-AC 9462");
        }

        public Task Execute(IJobExecutionContext context)
        {
            int bytesRecived = Convert.ToInt32(_networkCounter.NextValue());
            var time = DateTimeOffset.UtcNow;
            _repository.Create(new NetworkMetricModel()
            {
                Time = time,
                Value = bytesRecived
            });
            return Task.CompletedTask;
        }
    }
}
