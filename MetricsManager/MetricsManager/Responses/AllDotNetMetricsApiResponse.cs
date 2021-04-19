using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class AllDotNetMetricsApiResponse
    {
        List<DotNetMetricsApiDto> Metrics { get; set; }
    }
    public class DotNetMetricsApiDto
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
