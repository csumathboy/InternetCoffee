using csumathboy.Infrastructure.Caching;
using InternetCoffee.Application.Common.Caching;
using InternetCoffee.Application.Common.Weather;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
 
using System.Runtime.CompilerServices;

namespace InternetCoffee.Infrastructure.Weather;

internal static class Startup
{
    internal static IServiceCollection AddWeatherSevices(this IServiceCollection services, IConfiguration config)
    {
         // Register WeatherService with WeatherSettings injected
        services.AddTransient<IWeatherService, WeatherService>();
        services.AddHttpClient<IWeatherService, WeatherService>();
        return services;
    }
}