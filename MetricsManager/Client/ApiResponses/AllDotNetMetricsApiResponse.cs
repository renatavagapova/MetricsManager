using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Client.ApiResponses
{
    public class AllDotNetMetricsApiResponse
    {
        public List<DotNetMetricApiResponse> Metrics { get; set; }
    }
    public class DotNetMetricApiResponse
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
        public int IdAgent { get; set; }
    }
}
