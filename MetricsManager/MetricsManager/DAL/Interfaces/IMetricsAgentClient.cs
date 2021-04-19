using MetricsManager.Requests;
using MetricsManager.Responses;

namespace MetricsManager.DAL.Interfaces
{
    public interface IMetricsAgentClient
    {
        AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request);

        AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);

        AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);

        AllNetworkMetricsApiResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request);

        AllDotNetMetricsApiResponse GetDonNetMetrics(GetAllDotNetMetrisApiRequest request);
    }
}
