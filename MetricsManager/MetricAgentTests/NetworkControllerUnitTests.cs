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
    public class NetworkControllerUnitTests
    {
        private NetworkMetricsController _controller;
        private Mock<INetworkMetricsRepository> _mock;
        private Mock<ILogger<NetworkMetricsController>> _mocklogger;

        public NetworkControllerUnitTests()
        {
            _mock = new Mock<INetworkMetricsRepository>();
            _mocklogger = new Mock<ILogger<NetworkMetricsController>>();
            _controller = new NetworkMetricsController(_mocklogger.Object,_mock.Object);
        }

        [Fact]
        public void Create_Mock_Test()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<NetworkMetric>())).Verifiable();

            var result = _controller.Create(new NetworkMetricCreateRequest { Time = TimeSpan.FromSeconds(11), Value = 12 });
            _mock.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()), Times.AtMostOnce());
        }
    }
}
