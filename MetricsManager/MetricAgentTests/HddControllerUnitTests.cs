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
    public class HddControllerUnitTests
    {
        private HddMetricsController _controller;
        private Mock<IHddMetricsRepository> _mock;
        private Mock<ILogger<HddMetricsController>> _mocklogger;

        public HddControllerUnitTests()
        {
            _mock = new Mock<IHddMetricsRepository>();
            _mocklogger = new Mock<ILogger<HddMetricsController>>();
            _controller = new HddMetricsController(_mocklogger.Object,_mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<HddMetric>())).Verifiable();

            var result = _controller.Create(new HddMetricCreateRequest { Time = TimeSpan.FromSeconds(2), Value = 33 });
            _mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());
        }
    }
}
