using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using MetricsManager;

namespace MetricAgentTests
{
    public class CpuControllerUnitTests
    {
        private CpuMetricsController _controller;

        public CpuControllerUnitTests()
        {
            _controller = new CpuMetricsController();
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = _controller.GetMetrics(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsByPercentile_ReturnsOk()
        {            
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var percentile = Percentile.P75;

            var result = _controller.GetMetricsByPercentile(fromTime, toTime, percentile);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
