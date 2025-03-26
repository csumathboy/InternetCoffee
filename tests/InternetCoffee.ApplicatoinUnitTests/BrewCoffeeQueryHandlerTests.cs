using InternetCoffee.Application.BrewCoffee;
using InternetCoffee.Infrastructure.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
 

namespace InternetCoffee.ApplicationUnitTests
{
    /// <summary>
    /// BrewCoffeeQueryHandlerTests
    /// </summary>
    public class BrewCoffeeQueryHandlerTests
    {
        private readonly LocalCacheService _cacheService;
        private readonly IMemoryCache _memoryCache;
        private readonly BrewCoffeeQueryHandler _handler;
        public BrewCoffeeQueryHandlerTests()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _cacheService = new LocalCacheService(_memoryCache);
            _handler = new BrewCoffeeQueryHandler(_cacheService);
        }
        
        [Fact]
        public async Task Handle_ReturnsTeapot_OnAprilFirst()
        {
            var request = new BrewCoffeeRequest();
            request.requestTime = new DateTime(2025, 4, 1);
            var cancellationToken = new CancellationToken();
            var result = await _handler.Handle(request, cancellationToken);
            Assert.Equal(StatusCodes.Status418ImATeapot, result.StatusCode);
        }

        [Fact]
        public async Task Handle_ReturnsServiceUnavailable_EveryFifthRequest()
        {
            var request = new BrewCoffeeRequest();
            var cancellationToken = new CancellationToken();

            for (int i = 1; i < 5; i++)
            {
                await _handler.Handle(request, cancellationToken);
            }
            //Fifth request should return ServiceUnavailable
            var result = await _handler.Handle(request, cancellationToken);
            Assert.Equal(StatusCodes.Status503ServiceUnavailable, result.StatusCode);

        }

        [Fact]
        public async Task Handle_ReturnsOk_WhenNotAprilFirstOrFifthRequest()
        {
            var request = new BrewCoffeeRequest();
            var cancellationToken = new CancellationToken();

            var result = await _handler.Handle(request, cancellationToken);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal("Your piping hot coffee is ready", result.Message);
        }

        private class FactAttribute : Attribute
        {
        }
    }
}