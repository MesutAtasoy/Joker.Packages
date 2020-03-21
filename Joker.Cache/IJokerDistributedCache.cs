using System;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

namespace Joker.Cache
{
    public interface IJokerDistributedCache
    {
        Task<T> GetSetAsync<T>(string key, int cacheLifeTime, Func<Task<T>> func);
        Task SetObjectAsync<T>(string key, int cacheLifeTime, T value);
        void SetObject<T>(string key,int cacheLifeTime,  T value);
        Task<T> GetObjectAsync<T>(string key);
        T GetObject<T>(string key);
        Task<bool> ExistObjectAsync<T>(string key);
        bool ExistObject<T>(string key);
        void Remove(string key);
        Task RemoveAsync(string key);
    }
}
