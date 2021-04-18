using System;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class AllHddMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
    public class HddMetricDto
    {
        public int Value { get; set; }

        public int Id { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
