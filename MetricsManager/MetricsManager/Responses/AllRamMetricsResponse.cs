using System;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class AllRamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
    public class RamMetricDto
    {
        public int Value { get; set; }

        public int Id { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
