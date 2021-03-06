//using AutoMapper;
//using MetricsAgent;
//using MetricsAgent.Controllers;
//using MetricsAgent.DAL.Interface;
//using MetricsAgent.DAL.Model;
//using Microsoft.Extensions.Logging;
//using Moq;
//using System;
//using System.Collections.Generic;
//using Xunit;

//namespace MetricAgentTests
//{
//    public class CpuControllerUnitTests
//    {
//        private CpuMetricsController _controller;
//        private Mock<ICpuMetricsRepository> _mock;        
//        private Mock<ILogger<CpuMetricsController>> _mocklogger;        

//        public CpuControllerUnitTests()
//        {
//            var myProfile = new MapperProfile();
//            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
//            var mapper = new Mapper(configuration);

//            _mock = new Mock<ICpuMetricsRepository>();
//            _mocklogger = new Mock<ILogger<CpuMetricsController>>();
//            _controller = new CpuMetricsController(_mocklogger.Object, _mock.Object, mapper);

//        }

//        private List<CpuMetric> GetTestUsers()
//        {
//            var users = new List<CpuMetric>
//            {
//                new CpuMetric { Id = 1, Time = TimeSpan.FromSeconds(1), Value = 22 },
//                new CpuMetric { Id = 2, Time = TimeSpan.FromSeconds(10), Value = 33 },
//                new CpuMetric { Id = 3, Time = TimeSpan.FromSeconds(20), Value = 11 },
//                new CpuMetric { Id = 4, Time = TimeSpan.FromSeconds(11), Value = 24 }
//            };
//            return users;
//        }

//        [Fact]
//        public void Create_ShouldCall_Create_From_Repository()
//        {
//            // ????????????? ???????? ????????
//            // ? ???????? ??????????? ??? ? ??????????? ???????? CpuMetric ??????
//            _mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

//            // ????????? ???????? ?? ???????????
//            var result = _controller.Create(new MetricsAgent.Requests.CpuMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

//            // ????????? ???????? ?? ??, ??? ???? ??????? ??????????
//            // ????????????? ???????? ????? Create ??????????? ? ?????? ????? ??????? ? ?????????
//            _mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
//        }

//        [Fact]
//        public void GetAll()
//        {                    
//            _mock.Setup(repo => repo.GetAll()).Returns(GetTestUsers());
            
//            var result = _controller.GetAll();            
            
//            _mock.Verify(repository => repository.GetAll());
//        }
//    }
//}
