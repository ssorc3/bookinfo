using System;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ratings.Configuration;
using Ratings.Repositories;
using Ratings.Repositories.Mongo;
using Ratings.Services;

namespace Ratings
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Ratings Service", Version = "v1"});
            });

            if (_configuration["DataSource"]
                .Equals(nameof(DataSources.Sql), StringComparison.InvariantCultureIgnoreCase))
            {
                throw new NotImplementedException();
            }
            else if (_configuration["DataSource"]
                .Equals(nameof(DataSources.Mongo), StringComparison.InvariantCultureIgnoreCase))
            {
                services.AddTransient<IRatingsRepository, MongoRatingsRepository>();
            }
            else
            {
                throw new Exception("Failed to initialize datasource");
            }

            services.AddTransient<IRatingsService, RatingsService>();
            services.Configure<MongoDatabaseSettings>(options => _configuration.GetSection("Mongo").Bind(options));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ratings Service v1"); });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
