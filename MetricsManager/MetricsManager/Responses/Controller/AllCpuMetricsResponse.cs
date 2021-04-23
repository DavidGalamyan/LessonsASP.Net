using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses.Controller
{
    public class AllCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }

    public class CpuMetricDto
    {
        public long Id { get; set; }
        public int AgentID { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
