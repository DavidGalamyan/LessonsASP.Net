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
    public class RamControllerUnitTests
    {
        private RamMetricsController _controller;
        private Mock<IRamMetricsRepository> _mock;
        private Mock<ILogger<RamMetricsController>> _mocklogger;

        private List<RamMetric> GetTestUsers()
        {
            var users = new List<RamMetric>
            {
                new RamMetric { Id = 1, Time = TimeSpan.FromSeconds(1), Value = 22 },
                new RamMetric { Id = 2, Time = TimeSpan.FromSeconds(10), Value = 33 },
                new RamMetric { Id = 3, Time = TimeSpan.FromSeconds(20), Value = 11 },
                new RamMetric { Id = 4, Time = TimeSpan.FromSeconds(11), Value = 24 }
            };
            return users;
        }

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

        [Fact]
        public void GetAll()
        {
            _mock.Setup(repo => repo.GetAll()).Returns(GetTestUsers());

            var result = _controller.GetAll();

            _mock.Verify(repository => repository.GetAll());
        }
    }
}
