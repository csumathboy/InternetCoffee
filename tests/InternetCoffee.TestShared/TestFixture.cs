using InternetCoffee.Application.Common.Caching;
using InternetCoffee.Application.Common.Weather;
using InternetCoffee.Infrastructure.Caching;
using InternetCoffee.Infrastructure.Weather;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetCoffee.TestShared
{
    public class TestFixture
    {
        public ServiceProvider ServiceProvider { get; }

        public TestFixture()
        {
            var serviceCollection = new ServiceCollection();
            // Register IMemoryCache
            serviceCollection.AddMemoryCache();

            // Create a configuration using an in-memory settings or appsettings.json
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                   { "WeatherSettings:WeatherApiKey", "0a1893672a85a029dabd5098bbbf9125" },  //for test
                   { "WeatherSettings:WeatherBaseUrl", "https://api.openweathermap.org/data/2.5/weather?appid={0}&q={1}"},
                   { "WeatherSettings:LocationApiKey", "20b96dca8b9a5d37b0355e9461c66e76eed30a2274422fa6213d9de6ffb2b34e"},  //for test
                   { "WeatherSettings:LocationBaseUrl", "https://api.ipinfodb.com/v3/ip-city/?key={0}&ip={1}"}
                })  
                .Build();

            // Register IConfiguration in the DI container
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            // Register ICacheService and its implementation
            serviceCollection.AddSingleton<ICacheService, LocalCacheService>();

            // Register your dependencies here
            serviceCollection.AddTransient<IWeatherService, WeatherService>();
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddHttpClient<IWeatherService, WeatherService>();

            // Build the service provider and store it
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
