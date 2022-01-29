using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Client.ApiResponses
{
    public class AllRamMetricsApiResponse
    {
        public List<RamMetricApiResponse> Metrics { get; set; }
    }
    public class RamMetricApiResponse
    {
        public DateTimeOffset Time { get; set; }
        public double Value { get; set; }
        public int Id { get; set; }
        public int IdAgent { get; set; }
    }
}
