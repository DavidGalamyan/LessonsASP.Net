using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class AllRamMetricsApiResponse
    {
        public List<RamMetricApiDto> Metrics { get; set; }
    }
    public class RamMetricApiDto
    {
        public long Id { get; set; }

        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
