﻿using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Joker.AspNetCore.Extensions;

public static class HostingEnvironmentExtensions
{
    public static IConfiguration Configuration(this IWebHostEnvironment environment)
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("AppSettings.json")
            .AddJsonFile($"AppSettings.{environment.EnvironmentName}.json")
            .AddEnvironmentVariables()
            .Build();
    }
}