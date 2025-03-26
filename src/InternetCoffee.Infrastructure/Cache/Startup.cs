using InternetCoffee.Application.Common.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetCoffee.Infrastructure.Caching;

internal static class Startup
{
    internal static IServiceCollection AddCaching(this IServiceCollection services)
    {
        services.AddTransient<ICacheService, LocalCacheService>();

        services.AddMemoryCache();
        return services;
    }
}