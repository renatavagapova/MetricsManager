using System;
using System.Collections.Generic;
using MetricsManager.DAL.Models;

namespace MetricsManager.DAL.Interfaces
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetricModel>
    {
        IList<DotNetMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime);
        IList<DotNetMetricModel> GetMetricsFromTimeToTimeFromAgent(DateTimeOffset fromTime, DateTimeOffset toTime, int idAgent);
        IList<DotNetMetricModel> GetMetricsFromTimeToTimeOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string sortingField);
        IList<DotNetMetricModel> GetMetricsFromTimeToTimeFromAgentOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string sortingField, int idAgent);
        DateTimeOffset LastTime();
    }
}