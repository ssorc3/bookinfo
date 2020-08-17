using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using Reviews.Models;
using static Reviews.Extensions.ErrorHandling;

namespace Gateway
{
    public class HttpReviewsService : IReviewsService
    {
        private readonly HttpClient _client;
        private readonly ILogger<HttpReviewsService> _logger;

        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        public HttpReviewsService(HttpClient client, ILogger<HttpReviewsService> logger)
        {
            _client = client;
            _logger = logger;
        }
        public async Task<List<Review>> GetReviews(int productId)
        {
            try
            {
                var response = await _client.GetAsync($"reviews/{productId}");
                if (!response.IsSuccessStatusCode) return new List<Review>();
                var body = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<Review>>(body, _serializerOptions);
            }
            catch (TaskCanceledException e) when (True(() => _logger.LogError(e, "Http request timed out.")))
            {
                return new List<Review>();
            }
            catch (BrokenCircuitException e) when (True(() => _logger.LogError(e, "Circuit is broken.")))
            {
                return new List<Review>();
            }
        }
    }
}