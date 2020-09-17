using System;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

namespace Joker.Cache
{
    public interface IJokerDistributedCache
    {
        Task<T> GetSetAsync<T>(string key, int cacheLifeTime, Func<Task<T>> func);
        Task SetAsync<T>(string key, int cacheLifeTime, T value);
        Task SetAsync(string key, int cacheLifeTime, object value);
        void Set<T>(string key,int cacheLifeTime,  T value);
        void Set(string key,int cacheLifeTime,  object value);
        Task<T> GetAsync<T>(string key);
        T Get<T>(string key);
        Task<string> GetStringAsync(string key);
        string Get(string key);
        Task<bool> ExistObjectAsync<T>(string key);
        bool ExistObject<T>(string key);
        void Remove(string key);
        Task RemoveAsync(string key);
    }
}
