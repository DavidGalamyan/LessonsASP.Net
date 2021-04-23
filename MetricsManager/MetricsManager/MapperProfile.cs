using AutoMapper;
using MetricsManager.DAL.Models;
using MetricsManager.Responses;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricsApiDto>();
            CreateMap<DotNetMetric, DotNetMetricsApiDto>();
            CreateMap<HddMetric, HddMetricsApiDto>();
            CreateMap<RamMetric, RamMetricsApiDto>();
            CreateMap<NetworkMetric, NetworkMetricsApiDto>();
        }
    }
}
