using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using System.Linq;
using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Joker.Shared.Initializers;
using Joker.Mvc.Filters;
using Joker.Mvc.Middlewares;

namespace Joker.Mvc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJokerHttpContextAccessor(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
        public static IMvcCoreBuilder AddJokerMvcCore(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IStartupInitializer, StartupInitializer>();

            return services
                .AddMvcCore(options =>
                {
                    options.Filters.Add(typeof(ValidateModelStateAttribute));
                })
                .AddDataAnnotations()
                .AddApiExplorer()
                .AddDefaultJsonOptions()
                .AddAuthorization()
                .AddFluentValidation();
        }

        public static IMvcBuilder AddJokerMvc(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IStartupInitializer, StartupInitializer>();

            return services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(ValidateModelStateAttribute));
                })
                .AddDefaultJsonOptions()
                .AddFluentValidation();
        }

        public static IMvcCoreBuilder AddDefaultJsonOptions(this IMvcCoreBuilder builder)
           => builder.AddNewtonsoftJson(o =>
           {
               o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
               o.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
               o.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
               o.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
               o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
               o.SerializerSettings.Formatting = Formatting.Indented;
           });

        public static IMvcBuilder AddDefaultJsonOptions(this IMvcBuilder builder)
            => builder.AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                o.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                o.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                o.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                o.SerializerSettings.Formatting = Formatting.Indented;
            });

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

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ErrorHandlerMiddleware>();

        public static IApplicationBuilder UseAllForwardedHeaders(this IApplicationBuilder builder)
            => builder.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

    }
}
