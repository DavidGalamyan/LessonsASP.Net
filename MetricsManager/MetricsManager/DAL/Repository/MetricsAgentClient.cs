using MetricsManager.DAL.Interfaces;
using MetricsManager.Requests;
using MetricsManager.Responses;
using NLog;
using System;
using System.Net.Http;
using System.Text.Json;

namespace MetricsManager.DAL.Repository
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public MetricsAgentClient(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request)
        {            
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentAddress}/api/metrics/hdd/from/{request.FromTime.ToString("O")}/to/{request.ToTime.ToString("O")}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllHddMetricsApiResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                 _logger.Error(ex,"Чет пошло не так");
            }
            return null;
        }
        public AllNetworkMetricsApiResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentAddress}/api/metrics/network/from/{request.FromTime.ToString("O")}/to/{request.ToTime.ToString("O")}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllNetworkMetricsApiResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Чет пошло не так");
            }
            return null;
        }
        public AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentAddress}/api/metrics/ram/from/{request.FromTime.ToString("O")}/to/{request.ToTime.ToString("O")}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllRamMetricsApiResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Чет пошло не так");
            }
            return null;
        }
        public AllCpuMetricsApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.ToString("O");
            var toParameter = request.ToTime.ToString("O");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentAddress}/api/metrics/cpu/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllCpuMetricsApiResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Чет пошло не так");
            }
            return null;
        }
        public AllDotNetMetricsApiResponse GetAllDotNetMetrics(GetAllDotNetMetricsApiRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentAddress}/api/metrics/dotnet/from/{request.FromTime.ToString("O")}/to/{request.ToTime.ToString("O")}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllDotNetMetricsApiResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Чет пошло не так");
            }
            return null;
        }
    }
}
