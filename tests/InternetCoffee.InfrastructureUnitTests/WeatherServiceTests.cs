using InternetCoffee.Application.Common.Weather;
using InternetCoffee.Infrastructure.Caching;
using InternetCoffee.Infrastructure.Weather;
using InternetCoffee.TestShared;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace InternetCoffee.InfrastructureUnitTests
{
    public class WeatherServiceTests : IClassFixture<TestFixture>
    {
        private readonly IWeatherService? _weatherService;

        public WeatherServiceTests(TestFixture fixture)
        {
            _weatherService = fixture.ServiceProvider.GetService<IWeatherService>();
        }

        [Fact]
        public async Task GetUserCityAsync_ShouldEqualToDefinedIp()
        {
            string userip = "49.184.109.110"; // Melbourne IP
            string city = await _weatherService.GetUserCityAsync(userip);

            Assert.NotNull(city);
            Assert.Equal("Melbourne",city);
        }

        [Fact]
        public async Task GetCurrentTemperatureAsync_ShouldEqualToDefinedCity()
        {
            string city = "Melbourne"; // Melbourne IP
            var temperature = await _weatherService.GetCurrentTemperatureAsync(city);

            Assert.InRange(temperature,1,50);// Melbourne temperature range 1-50
        }

    }
}