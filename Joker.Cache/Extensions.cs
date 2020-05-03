using System;
using Microsoft.Extensions.DependencyInjection;

namespace Joker.Cache
{
    public static class Extensions
    {
        public static IServiceCollection AddJokerCache(this IServiceCollection services, Action<JokerCacheOptions> options)
        {
            var jokerCacheOptions = new JokerCacheOptions(); 
            
            options.Invoke(jokerCacheOptions);
            
            if(string.IsNullOrEmpty(jokerCacheOptions.Instance))
                throw new ArgumentNullException(nameof(jokerCacheOptions.Instance));
            
            if(string.IsNullOrEmpty(jokerCacheOptions.ConnectionString))
                throw new ArgumentNullException(nameof(jokerCacheOptions.ConnectionString));
                
            services.AddDistributedRedisCache(o =>
            {
                o.Configuration = jokerCacheOptions.ConnectionString;
                o.InstanceName = jokerCacheOptions.Instance;
            });

            services.AddTransient<IJokerDistributedCache, JokerDistributedCache>();
            return services;
        }
    }
}
