using System.Reflection;
using InternetCoffee.Application.BrewCoffee;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(BrewCoffeeQueryHandler).Assembly));

    }
}
