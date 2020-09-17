using System;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Joker.Cache
{
    public class JokerDistributedCache : IJokerDistributedCache
    {
        private readonly IDistributedCache _distributedCache;

        public JokerDistributedCache(IDistributedCache distributedCache)
            => _distributedCache = distributedCache;

        public async Task SetAsync<T>(string key, int cacheLifeTime, T value)
        {
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(value), SetDistributedCacheEntryOptions(cacheLifeTime));
        }
        public async Task SetAsync(string key, int cacheLifeTime, object value)
        {
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(value), SetDistributedCacheEntryOptions(cacheLifeTime));
        }
        public void Set<T>(string key, int cacheLifeTime, T value)
        {
            _distributedCache.SetString(key, JsonConvert.SerializeObject(value), SetDistributedCacheEntryOptions(cacheLifeTime));
        }
        public void Set(string key, int cacheLifeTime, object value)
        {
            _distributedCache.SetString(key, JsonConvert.SerializeObject(value), SetDistributedCacheEntryOptions(cacheLifeTime));
        }
        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _distributedCache.GetStringAsync(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
        public async Task<string> GetStringAsync(string key)
        {
            return await _distributedCache.GetStringAsync(key);
        }
        public T Get<T>(string key)
        {
            var value = _distributedCache.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
        public string Get(string key)
        {
            return _distributedCache.GetString(key);
        }
        public async Task<T> GetSetAsync<T>(string key, int cacheLifeTime, Func<Task<T>> func)
        {
            if (ExistObject<T>(key))
            {
                return await GetAsync<T>(key);
            }
            else
            {
                var value = await func.Invoke();
                await SetAsync<T>(key, cacheLifeTime, value);
                return value;
            }
        }
        public async Task<bool> ExistObjectAsync<T>(string key)
        {
            var value = await _distributedCache.GetStringAsync(key);
            return value != null;
        }
        public bool ExistObject<T>(string key)
        {
            var value = _distributedCache.GetString(key);
            return value != null;
        }
        public void Remove(string key)
        {
            _distributedCache.Remove(key);
        }
        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }
        private DistributedCacheEntryOptions SetDistributedCacheEntryOptions(int time)
        {
            return new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = new TimeSpan(0, 0, time)
            };
        }
    }
}