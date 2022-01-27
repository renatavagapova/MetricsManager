using MetricsAgent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Quartz;
using MetricsAgent.Models;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly IHddMetricsRepository _repository;
        private readonly PerformanceCounter _hddCounter;

        public HddMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IHddMetricsRepository>();
            _hddCounter = new PerformanceCounter("LogicalDisk", "Free Megabytes", "C:");
        }

        public Task Execute(IJobExecutionContext context)
        {
            double hddFreeSpace = _hddCounter.NextValue() / 1024 / 1024;
            var time = DateTimeOffset.UtcNow;
            _repository.Create(new HddMetricModel()
            {
                Time = time,
                FreeSize = hddFreeSpace
            });
            return Task.CompletedTask;
        }
    }
}
