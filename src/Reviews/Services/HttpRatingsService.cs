using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using static Reviews.Extensions.ErrorHandling;

namespace Reviews.Services
{
    public class HttpRatingsService : IRatingsService
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public HttpRatingsService(HttpClient client, ILogger<HttpRatingsService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<Dictionary<string, int>> GetRatings(int productId)
        {
            try
            {
                var response = await _client.GetAsync($"ratings");
                if (!response.IsSuccessStatusCode) return new Dictionary<string, int>();
                var body = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<Dictionary<string, int>>(body, _serializerOptions);
            }
            catch (TaskCanceledException e) when (True(() => _logger.LogError(e, "Http request timed out.")))
            {
                return new Dictionary<string, int>();
            }
            catch (BrokenCircuitException e) when (True(() => _logger.LogError(e, "Circuit is broken.")))
            {
                return new Dictionary<string, int>();
            }
        }
    }
}