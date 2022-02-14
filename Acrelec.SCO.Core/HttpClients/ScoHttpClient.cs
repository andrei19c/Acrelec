using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Acrelec.SCO.Core.Interfaces;
using Acrelec.SCO.Core.Model.RestExchangedMessages;
using Microsoft.Extensions.Configuration;

namespace Acrelec.SCO.Core.HttpClients
{
    public class ScoHttpClient : IScoHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogService _logService;
        private readonly IConfiguration _configuration;
        public ScoHttpClient(HttpClient httpClient, ILogService logService, IConfiguration configuration)
        {
            _logService = logService;
            _configuration = configuration;

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri($"{configuration["ScoServerSettings:Url"]}:{configuration["ScoServerSettings:Port"]}");
        }
        public async Task<InjectOrderResponse> AddOrder(InjectOrderRequest order)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            httpRequestMessage.Content = new StringContent(JsonSerializer.Serialize(order), System.Text.Encoding.UTF8, "application/json");
            httpRequestMessage.RequestUri = new Uri($"{_httpClient.BaseAddress}api-sco/v1/injectorder");

            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
                var result = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<InjectOrderResponse>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
                else
                {
                    _logService.Print($"###Error: {httpResponseMessage.StatusCode} - {httpResponseMessage.RequestMessage} - Details: {result}");
                }

            }
            catch (Exception ex)
            {
                _logService.Print($"###Error: {ex.Message}");
            }
            return new InjectOrderResponse { OrderNumber = string.Empty };
        }

        public async Task<CheckAvailabilityResponse> IsAvailable()
        {
            try
            {
                var httpResponseMessage = await _httpClient.GetAsync("api-sco/v1/availability");
                var result = await httpResponseMessage.Content.ReadAsStringAsync();

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<CheckAvailabilityResponse>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
                else
                {
                    _logService.Print($"###Error: {httpResponseMessage.StatusCode} - {httpResponseMessage.RequestMessage} - Details: {result}");
                    return new CheckAvailabilityResponse
                    {
                        CanInjectOrders = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logService.Print($"####Error: {ex.Message}");
                return new CheckAvailabilityResponse
                {
                    CanInjectOrders = false
                };
            }

        }
    }
}
