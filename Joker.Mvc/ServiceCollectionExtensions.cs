using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Joker.Mvc.Initializers;
using MediatR;

namespace Joker.Mvc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJokerMediatr(this IServiceCollection services,
            params Type[] assemblyPointerTypes)
        {
            services.AddMediatR(assemblyPointerTypes);
            services.AddValidatorsFromAssembly(assemblyPointerTypes.First().Assembly);

            return services;
        }
        
        public static IServiceCollection AddJokerHttpContextAccessor(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            return services;
        }

        public static IServiceCollection AddStartupInitializer(this IServiceCollection services)
        {
            services.AddScoped<IStartupInitializer, StartupInitializer>();
            return services;
        }

        public static IServiceCollection AddApiBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            return services;
        }

        public static IServiceCollection AddInitializers(this IServiceCollection services, params Type[] initializers)
          => initializers == null
              ? services
              : services.AddTransient<IStartupInitializer, StartupInitializer>(c =>
              {
                  var startupInitializer = new StartupInitializer();
                  var validInitializers = initializers.Where(t => typeof(IInitializer).IsAssignableFrom(t));
                  foreach (var initializer in validInitializers)
                  {
                      startupInitializer.AddInitializer(c.GetService(initializer) as IInitializer);
                  }

                  return startupInitializer;
              });
    }
}
