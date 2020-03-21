using Joker.Shared.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Joker.Cache
{
    public static class Extensions
    {
        private static readonly string SectionName = "cache";

        public static IServiceCollection AddJokerCache(this IServiceCollection services)
        {
            IConfiguration _configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                _configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.AddOption<JokerCacheOptions>(SectionName);
            var options = _configuration.GetOptions<JokerCacheOptions>(SectionName);
            services.AddDistributedRedisCache(o =>
            {
                o.Configuration = options.ConnectionString;
                o.InstanceName = options.Instance;
            });

            services.AddTransient<IJokerDistributedCache, JokerDistributedCache>();
            return services;
        }
    }
}
