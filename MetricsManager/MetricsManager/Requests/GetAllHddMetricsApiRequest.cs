using System;

namespace MetricsManager.Requests
{
    public class GetAllHddMetricsApiRequest
    {
        public DateTime FromTime { get; set; }

        public DateTime ToTime { get; set; }

        public string AgentAddress { get; set; }
    }
}
