using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(BlazorApp.Api.Startup))]

namespace BlazorApp.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["RediConnection"];
            });
        }
    }
}