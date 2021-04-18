//using MetricsManager.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;
//using System;
//using Xunit;

//namespace MetricsManagerTests
//{
//    public class DotNetControllerUnitTests
//    {
//        private DotNetMetricsController _controller;
//        private Mock<ILogger<DotNetMetricsController>> _logger;

//        public DotNetControllerUnitTests()
//        {
//            _logger = new Mock<ILogger<DotNetMetricsController>>();
//            _controller = new DotNetMetricsController(_logger.Object);
//        }

//        [Fact]
//        public void GetMetricsFromAgent_ReturnsOk()
//        {
//            var agentId = 1;
//            var fromTime = TimeSpan.FromSeconds(0);
//            var toTime = TimeSpan.FromSeconds(100);

//            var result = _controller.GetMetricsFromAgent(agentId, fromTime, toTime);

//            _ = Assert.IsAssignableFrom<IActionResult>(result);
//        }
//        [Fact]
//        public void GetMetricsFromAllCluset_ReturnsOk()
//        {
            
//            var fromTime = TimeSpan.FromSeconds(0);
//            var toTime = TimeSpan.FromSeconds(100);

//            var result = _controller.GetMetricsFromAllCluster(fromTime, toTime);

//            _ = Assert.IsAssignableFrom<IActionResult>(result);
//        }
//    }
//}
