using System;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class AllNetworkMetricsResponse
    {
        public List<NetworkMetricDto> Metrics {get;set;}
    }

    public class NetworkMetricDto
    {
        public int Value { get; set; }

        public int Id { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
