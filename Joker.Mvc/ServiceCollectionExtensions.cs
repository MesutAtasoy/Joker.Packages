using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Joker.Mvc.Behaviors;
using Microsoft.AspNetCore.Mvc;
using Joker.Mvc.Initializers;
using MediatR;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Joker.Mvc;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJokerMediatr(this IServiceCollection services,
        params Type[] assemblyPointerTypes)
    {
        services.AddMediatR(assemblyPointerTypes);
        services.AddValidatorsFromAssembly(assemblyPointerTypes.First().Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
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
        
    public static IServiceCollection AddApiVersion(this IServiceCollection services, IApiVersionReader apiVersionReader = null)
        => services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
            config.ApiVersionReader = apiVersionReader ?? new HeaderApiVersionReader("X-Api-Version");
        });

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