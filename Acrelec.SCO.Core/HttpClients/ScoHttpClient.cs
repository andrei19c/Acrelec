using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Acrelec.SCO.Core.Interfaces;
using Acrelec.SCO.Core.Model.RestExchangedMessages;

namespace Acrelec.SCO.Core.HttpClients
{
    public class ScoHttpClient : IScoHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogService _logService;
        public ScoHttpClient(HttpClient httpClient, ILogService logService)
        {
            _logService = logService;

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:4500/");
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
                var result = httpResponseMessage.Content.ReadAsStringAsync();

                if (result.IsCompletedSuccessfully)
                {
                    return JsonSerializer.Deserialize<InjectOrderResponse>(result.Result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
            }
            catch(Exception ex)
            {
                _logService.Print($"###Error: {ex.Message}");
            }
            return new InjectOrderResponse { OrderNumber = string.Empty };
        }

        public async Task<bool> IsAvailable()
        {
            var result = await _httpClient.GetAsync("api-sco/v1/availability");
            try
            {
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logService.Print($"####Error: {ex.Message}");
            }
            return false;
        }
    }
}
