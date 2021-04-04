using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class RamControllerUnitTests
    {
        private RamMetricsController _controller;
        private Mock<ILogger<RamMetricsController>> _logger;

        public RamControllerUnitTests()
        {
            _logger = new Mock<ILogger<RamMetricsController>>();
            _controller = new RamMetricsController(_logger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;

            var result = _controller.GetMetricsFromAgent(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            var result = _controller.GetMetricsFromAllCluster();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
