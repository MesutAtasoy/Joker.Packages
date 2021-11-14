using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Joker.AspNetCore.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerDefault(this IServiceCollection services,
        Action<SwaggerGenOptions> options = null)
        => services.AddSwaggerGen(options);

    public static IServiceCollection AddCorsAny(this IServiceCollection services, string policyName = "CorsPolicy")
        => services.AddCors(options =>
        {
            options.AddPolicy(policyName, cors =>
                cors.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });
}