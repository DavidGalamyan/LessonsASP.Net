using MetricsAgent.Controllers;
using System;
using Xunit;
using Moq;
using MetricsAgent.DAL.Model;
using Microsoft.Extensions.Logging;
using MetricsAgent.Requests;
using MetricsAgent.DAL.Interface;
using System.Collections.Generic;
using MetricsAgent;
using AutoMapper;

namespace MetricAgentTests
{
    public class HddControllerUnitTests
    {
        private HddMetricsController _controller;
        private Mock<IHddMetricsRepository> _mock;
        private Mock<ILogger<HddMetricsController>> _mocklogger;

        private List<HddMetric> GetTestUsers()
        {
            var users = new List<HddMetric>
            {
                new HddMetric { Id = 1, Time = TimeSpan.FromSeconds(1), Value = 22 },
                new HddMetric { Id = 2, Time = TimeSpan.FromSeconds(10), Value = 33 },
                new HddMetric { Id = 3, Time = TimeSpan.FromSeconds(20), Value = 11 },
                new HddMetric { Id = 4, Time = TimeSpan.FromSeconds(11), Value = 24 }
            };
            return users;
        }

        public HddControllerUnitTests()
        {
            var myProfile = new MapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            _mock = new Mock<IHddMetricsRepository>();
            _mocklogger = new Mock<ILogger<HddMetricsController>>();
            _controller = new HddMetricsController(_mocklogger.Object,_mock.Object, mapper);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<HddMetric>())).Verifiable();

            var result = _controller.Create(new HddMetricCreateRequest { Time = TimeSpan.FromSeconds(2), Value = 33 });
            _mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());
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
