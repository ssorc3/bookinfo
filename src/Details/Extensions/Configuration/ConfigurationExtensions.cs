using System;
using Microsoft.Extensions.Configuration;

namespace Details.Extensions.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder ConfigureDefaults(this IConfigurationBuilder builder)
        {
            builder.AddEnvironmentVariables()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .AddUserSecrets<Program>();
            return builder;
        }
    }
}