using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricAgentTests
{
    public class HddControllerUnitTests
    {
        private HddMetricsController _controller;

        public HddControllerUnitTests()
        {
            _controller = new HddMetricsController();
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            var result = _controller.GetMetrics();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
