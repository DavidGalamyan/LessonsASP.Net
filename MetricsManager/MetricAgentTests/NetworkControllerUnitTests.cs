using MetricsAgent.Controllers;
using System;
using Xunit;
using Moq;
using MetricsAgent.DAL.Model;
using Microsoft.Extensions.Logging;
using MetricsAgent.Requests;
using MetricsAgent.DAL.Interface;
using System.Collections.Generic;

namespace MetricAgentTests
{
    public class NetworkControllerUnitTests
    {
        private NetworkMetricsController _controller;
        private Mock<INetworkMetricsRepository> _mock;
        private Mock<ILogger<NetworkMetricsController>> _mocklogger;

        private List<NetworkMetric> GetTestUsers()
        {
            var users = new List<NetworkMetric>
            {
                new NetworkMetric { Id = 1, Time = TimeSpan.FromSeconds(1), Value = 22 },
                new NetworkMetric { Id = 2, Time = TimeSpan.FromSeconds(10), Value = 33 },
                new NetworkMetric { Id = 3, Time = TimeSpan.FromSeconds(20), Value = 11 },
                new NetworkMetric { Id = 4, Time = TimeSpan.FromSeconds(11), Value = 24 }
            };
            return users;
        }

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

        [Fact]
        public void GetAll()
        {
            _mock.Setup(repo => repo.GetAll()).Returns(GetTestUsers());

            var result = _controller.GetAll();

            _mock.Verify(repository => repository.GetAll());
        }
    }
}
