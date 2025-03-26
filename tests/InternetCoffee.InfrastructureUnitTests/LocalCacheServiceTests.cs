using InternetCoffee.Infrastructure.Caching;
using Microsoft.Extensions.Caching.Memory;
 
namespace InternetCoffee.InfrastructureUnitTests
{
    public class LocalCacheServiceTests
    {
        private readonly LocalCacheService _cacheService;
        private readonly IMemoryCache _memoryCache;

        public LocalCacheServiceTests()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _cacheService = new LocalCacheService(_memoryCache);
        }

        [Fact]
        public void SetData_ShouldStoreValueInCache()
        {
            string key = "testKey";
            string value = "testValue";
            _cacheService.SetData(key, value, DateTimeOffset.UtcNow.AddMinutes(5));

            var result = _cacheService.GetData<string>(key);
            Assert.Equal(value, result);
        }

        [Fact]
        public void GetData_ShouldReturnNull_WhenKeyDoesNotExist()
        {
            var result = _cacheService.GetData<string>("nonExistingKey");
            Assert.Null(result);
        }

        [Fact]
        public void RemoveData_ShouldRemoveValueFromCache()
        {
            string key = "testKey";
            _cacheService.SetData(key, "testValue", DateTimeOffset.UtcNow.AddMinutes(5));
            _cacheService.RemoveData(key);

            var result = _cacheService.GetData<string>(key);
            Assert.Null(result);
        }
    }
}