using System;
using Gateway.GraphQL;
using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static Reviews.Extensions.HttpPolicies;

namespace Gateway
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IReviewsService, HttpReviewsService>(client =>
            {
                client.BaseAddress = new Uri(Configuration["Reviews:BaseAddress"]);
            })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetCircuitBreakerPolicy());
            
            services.AddHttpClient<IDetailsService, HttpDetailsService>(client =>
            {
                client.BaseAddress = new Uri(Configuration["Details:BaseAddress"]);
            })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetCircuitBreakerPolicy());
            
            services.AddHealthChecks();
            services.AddGraphQL(
                SchemaBuilder.New()
                    .AddQueryType<Query>()
            );
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseGraphQL();
            app.UsePlayground();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
