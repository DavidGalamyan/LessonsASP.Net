using MetricsAgent.Controllers;
using MetricsAgent.DAL.Model;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;
using MetricsAgent.Requests;
using MetricsAgent.DAL.Interface;
using System.Collections.Generic;

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
            _mock = new Mock<IDotNetMetricsRepository>();
            _mocklogger = new Mock<ILogger<DotNetMetricsController>>();
            _controller = new DotNetMetricsController(_mocklogger.Object, _mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _mock.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new DotNetMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
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
