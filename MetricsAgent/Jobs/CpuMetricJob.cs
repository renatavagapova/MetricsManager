﻿using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using Quartz;
using MetricsAgent.DAL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly PerformanceCounter _cpuCounter;

        public CpuMetricJob(IServiceProvider provider)
        {
            _repository = provider.GetService<ICpuMetricsRepository>();
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());
            var time = DateTimeOffset.UtcNow;
            _repository.Create(new CpuMetricModel()
            {
                Time = time,
                Value = cpuUsageInPercents
            });
            return Task.CompletedTask;
        }
    }
}