using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Moq;
using MetricsAgent.Model;
using MetricsAgent.DAL;
using Microsoft.Extensions.Logging;
using MetricsAgent.Requests;

namespace MetricAgentTests
{
    public class RamControllerUnitTests
    {
        private RamMetricsController _controller;
        private Mock<IRamMetricsRepository> _mock;
        private Mock<ILogger<RamMetricsController>> _mocklogger;

        public RamControllerUnitTests()
        {
            _mock = new Mock<IRamMetricsRepository>();
            _mocklogger = new Mock<ILogger<RamMetricsController>>();
            _controller = new RamMetricsController(_mocklogger.Object,_mock.Object);
        }

        [Fact]
        public void Create_Mock_Test()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();
            var result = _controller.Create(new RamMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 123 });
            _mock.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
        }
    }
}
