using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Joker.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerDefault(this IServiceCollection services, Action<SwaggerGenOptions> options = null) 
            => services.AddSwaggerGen(options);

        public static IServiceCollection AddCorsAny(this IServiceCollection services, string PolicyName = "CorsPolicy")
            => services.AddCors(options =>
            {
                options.AddPolicy(PolicyName, cors =>
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
            });
    }
}
