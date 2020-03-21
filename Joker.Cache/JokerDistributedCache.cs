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

        public async Task SetObjectAsync<T>(string key, int cacheLifeTime, T value)
            => await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(value),
                SetDistributedCacheEntryOptions(cacheLifeTime));

        public void SetObject<T>(string key, int cacheLifeTime, T value)
            => _distributedCache.SetString(key, JsonConvert.SerializeObject(value),
                SetDistributedCacheEntryOptions(cacheLifeTime));

        public async Task<T> GetObjectAsync<T>(string key)
        {
            var value = await _distributedCache.GetStringAsync(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<T> GetSetAsync<T>(string key, int cacheLifeTime, Func<Task<T>> func)
        {
            if (ExistObject<T>(key))
            {
                return await GetObjectAsync<T>(key);
            }
            else
            {
                var value = await func.Invoke();
                await SetObjectAsync<T>(key, cacheLifeTime, value);
                return value;
            }
        }

        public T GetObject<T>(string key)
        {
            var value = _distributedCache.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
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
            => _distributedCache.Remove(key);

        public async Task RemoveAsync(string key)
            => await _distributedCache.RemoveAsync(key);


        private DistributedCacheEntryOptions SetDistributedCacheEntryOptions(int time)
        {
            return new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = new TimeSpan(0, 0, time)
            };
        }
    }
}