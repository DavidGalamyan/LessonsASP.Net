using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricAgentTests
{
    public class RamControllerUnitTests
    {
        private RamMetricsController _controller;

        public RamControllerUnitTests()
        {
            _controller = new RamMetricsController();
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            var result = _controller.GetMetrics();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
