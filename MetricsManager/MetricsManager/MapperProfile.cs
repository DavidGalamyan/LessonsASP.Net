using AutoMapper;
using MetricsManager.DAL.Models;
using MetricsManager.Responses;
using MetricsManager.Responses.Controller;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricApiDto>();
            CreateMap<DotNetMetric, DotNetMetricApiDto>();
            CreateMap<HddMetric, HddMetricApiDto>();
            CreateMap<RamMetric, RamMetricApiDto>();
            CreateMap<NetworkMetric, NetworkMetricApiDto>();

            CreateMap<CpuMetricApiDto, CpuMetric>();
            CreateMap<DotNetMetricApiDto, DotNetMetric>();
            CreateMap<HddMetricApiDto, HddMetric>();
            CreateMap<RamMetricApiDto, RamMetric>();
            CreateMap<NetworkMetricApiDto, NetworkMetric>();

            CreateMap<CpuMetric, CpuMetricDto>();
            CreateMap<DotNetMetric, DotNetMetricDto>();
            CreateMap<HddMetric, HddMetricDto>();
            CreateMap<RamMetric, RamMetricDto>();
            CreateMap<NetworkMetric, NetworkMetricDto>();
        }
    }
}
