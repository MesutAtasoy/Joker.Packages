using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Joker.AspNetCore.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSwaggerDefault(this IApplicationBuilder application)
        {
            application.UseSwagger();
            application.UseSwaggerUI(cfg => cfg.SwaggerEndpoint("/swagger/api/swagger.json", string.Empty));
        }

        public static void UseCorsDefault(this IApplicationBuilder app, string PolicyName = "CorsPolicy")
            => app.UseCors(PolicyName);

        public static void UseHstsDefault(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();
        }
        
        public static void UseHstsDefault(this IApplicationBuilder app, IWebHostEnvironment env, string errorHandlingPath)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(errorHandlingPath);
                app.UseHsts();
            }
        }
    }
}
