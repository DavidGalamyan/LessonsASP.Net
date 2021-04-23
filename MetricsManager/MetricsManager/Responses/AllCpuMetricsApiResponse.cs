using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class AllCpuMetricsApiResponse
    {
        public IList<CpuMetricsApiDto> Metrics { get; set; }
    }
    public class CpuMetricsApiDto
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
