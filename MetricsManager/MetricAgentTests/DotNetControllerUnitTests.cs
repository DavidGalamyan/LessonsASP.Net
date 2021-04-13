using MetricsAgent.Controllers;
using MetricsAgent.DAL.Model;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;
using MetricsAgent.Requests;
using MetricsAgent.DAL.Interface;
using System.Collections.Generic;
using AutoMapper;
using MetricsAgent;

namespace MetricAgentTests
{
    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController _controller;
        private Mock<IDotNetMetricsRepository> _mock;
        private Mock<ILogger<DotNetMetricsController>> _mocklogger;

        private List<DotNetMetric> GetTestUsers()
        {
            var users = new List<DotNetMetric>
            {
                new DotNetMetric { Id = 1, Time = TimeSpan.FromSeconds(1), Value = 22 },
                new DotNetMetric { Id = 2, Time = TimeSpan.FromSeconds(10), Value = 33 },
                new DotNetMetric { Id = 3, Time = TimeSpan.FromSeconds(20), Value = 11 },
                new DotNetMetric { Id = 4, Time = TimeSpan.FromSeconds(11), Value = 24 }
            };
            return users;
        }

        public DotNetControllerUnitTests()
        {
            var myProfile = new MapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            _mock = new Mock<IDotNetMetricsRepository>();
            _mocklogger = new Mock<ILogger<DotNetMetricsController>>();
            _controller = new DotNetMetricsController(_mocklogger.Object, _mock.Object, mapper);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            _mock.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();

            // ��������� �������� �� �����������
            var result = _controller.Create(new DotNetMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            _mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
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
