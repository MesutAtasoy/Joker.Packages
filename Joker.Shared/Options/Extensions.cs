using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Joker.Shared.Options
{
    public static class Extensions
    {
        public static IServiceCollection AddOption<TClass>(this IServiceCollection services, string sectionName) where TClass : class
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            services.Configure<TClass>(configuration.GetSection(sectionName));
            return services;
        }

        public static string Underscore(this string value)
         => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);
            return model;
        }
    }

}
