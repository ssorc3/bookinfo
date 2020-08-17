using System;
using System.Net.Http;
using Polly;

namespace Reviews.Extensions
{
    public static class HttpPolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return Policy.Handle<Exception>().CircuitBreakerAsync(1, TimeSpan.FromMinutes(1)).AsAsyncPolicy<HttpResponseMessage>();
        }
    }
}