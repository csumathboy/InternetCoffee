using InternetCoffee.Application.Common.Caching;
using Microsoft.Extensions.Caching.Memory;
 

namespace InternetCoffee.Infrastructure.Caching
{
    /// <summary>
    ///local cache service 
    /// </summary>
    public class LocalCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public LocalCacheService(IMemoryCache cache)
        {
            _memoryCache = cache;
        }
     
        /// <summary>
        /// get data from cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T? GetData<T>(string key){
            return _memoryCache.Get<T>(key);

        }
         
        /// <summary>
        /// remove data from cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object RemoveData(string key)
        {
            var res = true;
            if (!string.IsNullOrEmpty(key))
            {
                _memoryCache.Remove(key);
            }
            else
            {
                res = false;
            }
            return res;
        }
        /// <summary>
        /// set data to cache with expiration time
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        /// <returns></returns>
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var res = true;
            if (!string.IsNullOrEmpty(key))
            {
                _memoryCache.Set(key, value, expirationTime);
            }
            else
            {
                res = false;
            }
            return res;
        }

        /// <summary>
        /// set data to cache without expiration time
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        /// <returns></returns>
        public bool SetData<T>(string key, T value)
        {
            var res = true;
            if (!string.IsNullOrEmpty(key))
            {
                _memoryCache.Set(key, value);
            }
            else
            {
                res = false;
            }
            return res;
        }
    }
}
     