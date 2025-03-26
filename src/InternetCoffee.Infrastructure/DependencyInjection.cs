using InternetCoffee.Application.Common.Weather;
using InternetCoffee.Infrastructure.Caching;
using InternetCoffee.Infrastructure.Weather;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        //Register Caching
        builder.Services.AddCaching();
        
        //Register Weather
        builder.Services.AddWeatherSevices(builder.Configuration);

    }
}
