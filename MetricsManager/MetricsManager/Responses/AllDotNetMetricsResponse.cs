using System;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class AllDotNetMetricsResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }

    public class DotNetMetricDto
    {
        public int Value { get; set; }

        public int Id { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
