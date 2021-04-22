﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Responses
{
    public class AllHddMetricsApiResponse
    {
        public IList<HddMetricsApiDto> Metrics { get; set; }
    }
    public class HddMetricsApiDto
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
