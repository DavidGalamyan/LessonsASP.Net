using MetricsAgent.Controllers;
using System;
using Xunit;  
using Moq;
using MetricsAgent.DAL;
using Microsoft.Extensions.Logging;
using MetricsAgent.Model;

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

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            _mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // ��������� �������� �� �����������
            var result = _controller.Create(new MetricsAgent.Requests.CpuMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            _mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }
    }
}
