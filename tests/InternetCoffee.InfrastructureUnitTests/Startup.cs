using InternetCoffee.Application.Common.Weather;
using InternetCoffee.Infrastructure.Weather;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetCoffee.InfrastructureUnitTests
{
    public class Startup
    {
        public Startup()
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        public IConfigurationRoot Configuration { get; }
        public virtual void ConfigureServices(IServiceCollection services)
        {

            // Add your services to the container
            services.AddTransient<IWeatherService, WeatherService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddHttpClient<IWeatherService, WeatherService>();

        }
    }
}
