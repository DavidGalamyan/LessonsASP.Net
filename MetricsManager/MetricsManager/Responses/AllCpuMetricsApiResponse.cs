using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{

    public class AllCpuMetricsApiResponse
    {
        public List<CpuMetricApiDto> Metrics { get; set; }
    }

    public class CpuMetricApiDto
    {
        public long Id { get; set; }

        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
