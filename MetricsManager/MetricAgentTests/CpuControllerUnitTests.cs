using MetricsAgent.Controllers;
using System;
using Xunit;  
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.Model;
using MetricsAgent.DAL.Interface;
using System.Collections.Generic;

namespace MetricAgentTests
{
    public class CpuControllerUnitTests
    {
        private CpuMetricsController _controller;
        private Mock<ICpuMetricsRepository> _mock;        
        private Mock<ILogger<CpuMetricsController>> _mocklogger;

        public CpuControllerUnitTests()
        {
            _mock = new Mock<ICpuMetricsRepository>();
            _mocklogger = new Mock<ILogger<CpuMetricsController>>();
            _controller = new CpuMetricsController(_mocklogger.Object, _mock.Object);
        }

        private List<CpuMetric> GetTestUsers()
        {
            var users = new List<CpuMetric>
            {
                new CpuMetric { Id = 1, Time = TimeSpan.FromSeconds(1), Value = 22 },
                new CpuMetric { Id = 2, Time = TimeSpan.FromSeconds(10), Value = 33 },
                new CpuMetric { Id = 3, Time = TimeSpan.FromSeconds(20), Value = 11 },
                new CpuMetric { Id = 4, Time = TimeSpan.FromSeconds(11), Value = 24 }
            };
            return users;
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new MetricsAgent.Requests.CpuMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
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
