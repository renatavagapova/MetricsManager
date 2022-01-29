using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Client.ApiResponses
{
    public class AllNetworkMetricsApiResponse
    {
        public List<NetworkMetricApiResponse> Metrics { get; set; }
    }
    public class NetworkMetricApiResponse
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
        public int IdAgent { get; set; }
    }
}
