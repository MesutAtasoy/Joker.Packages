using System;
using System.Text;
using Joker.Authentication.Handlers;
using Joker.Authentication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Joker.Authentication
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, Action<JwtOptions> jwtOptions)
        {
            var options = new JwtOptions();
            jwtOptions.Invoke(options);
            ValidateJwtOptions(options);
            services.AddSingleton(options);
            services.AddSingleton<IJwtHandler, JwtHandler>();

            services.AddAuthentication(configureOptions =>
                {
                    configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    configureOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    
                })
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.SaveToken = true;
                    jwtBearerOptions.RequireHttpsMetadata = false;
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                        ValidateIssuer = true,
                        ValidIssuer = options.Issuer,
                        ValidateAudience = options.ValidateAudience,
                        ValidAudience = options.ValidAudience,
                        ValidateLifetime = options.ValidateLifetime,
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = true
                    };
                });

            return services;
        }

        private static void ValidateJwtOptions(JwtOptions jwtOptions)
        {
            if (string.IsNullOrEmpty(jwtOptions.Issuer))
                throw new ArgumentNullException(nameof(jwtOptions.Issuer));

            if (string.IsNullOrEmpty(jwtOptions.SecretKey))
                throw new ArgumentNullException(nameof(jwtOptions.SecretKey));
        }
    }
}