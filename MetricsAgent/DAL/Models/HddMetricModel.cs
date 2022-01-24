using System;

namespace MetricsAgent.Models
{
    public class HddMetricModel
    {
        public int Id { get; set; }
        public double FreeSize { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
