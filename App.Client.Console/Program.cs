using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace App.Client.Console
{
    // add logging dependency: https://www.blinkingcaret.com/2018/02/14/net-core-console-logging/
    // https://github.com/serilog/serilog-extensions-hosting
    // https://salanoi.wordpress.com/2017/09/16/test/
    // https://blog.danskingdom.com/Examples-of-setting-up-Serilog-in-Console-apps-and-ASP-Net-Core-3/
    //
    // Depdenency Injection Framework
    // > dotnet add package Microsoft.Extensions.DependencyInjection
    //
    // Logging
    // > dotnet add package Microsoft.Extensions.Logging
    //
    // Serilog
    // > dotnet add package Serilog
    // > dotnet add package Serilog.Extensions.Hosting
    // > dotnet add package Serilog.Sinks.File
    // > dotnet add package Serilog.Sinks.Console
    //
    // Splunk Logging
    // > dotnet add package Serilog.Sinks.Splunk
    //
    // Configuration Settings
    // > dotnet add package Microsoft.Extensions.Configuration
    // > dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables

    internal class Program
    {
        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            // test it
            var logger = serviceProvider.GetService<ILogger<Program>>();
            logger.LogInformation($"Hello World {System.DateTime.Now}");
        }
    }
}