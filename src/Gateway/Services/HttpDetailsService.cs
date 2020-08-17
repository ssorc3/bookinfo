using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Details.Models;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using static Reviews.Extensions.ErrorHandling;

namespace Gateway
{
    public class HttpDetailsService : IDetailsService
    {
        private readonly HttpClient _client;
        private readonly ILogger<HttpDetailsService> _logger;

        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public HttpDetailsService(HttpClient client, ILogger<HttpDetailsService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<BookDetails> GetBookDetails(int productId)
        {
            try
            {
                var response = await _client.GetAsync($"details/{productId}");
                if (!response.IsSuccessStatusCode) return new BookDetails();
                var body = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<BookDetails>(body, _serializerOptions);
            }
            catch (TaskCanceledException e) when (True(() => _logger.LogError(e, "Http request timed out.")))
            {
                return new BookDetails();
            }
            catch (BrokenCircuitException e) when (True(() => _logger.LogError(e, "Circuit is broken.")))
            {
                return new BookDetails();
            }
        }
    }
}