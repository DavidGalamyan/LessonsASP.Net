using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class AllRamMetricsApiResponse
    {
        public IList<RamMetricsApiDto> Metrics { get; set; }
    }
    public class RamMetricsApiDto
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
