using App.Core.Application;
using App.Core.Domain.Entities;
using App.Core.Domain.Repository;
using App.Infrastructure.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.IO;

#region ------- Package dependencies documentation -------
/////////////////////////////////////////////////////////////////////////////////////////
// https://www.blinkingcaret.com/2018/02/14/net-core-console-logging/
// https://github.com/serilog/serilog-extensions-hosting
// Usage: https://blog.danskingdom.com/Examples-of-setting-up-Serilog-in-Console-apps-and-ASP-Net-Core-3/
// Splunk: https://salanoi.wordpress.com/2017/09/16/test/
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
// > dotnet add package Microsoft.Extensions.Configuration.Binder
// > dotnet add package Microsoft.Extensions.Configuration.Json
// > dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables
// > dotnet add package Microsoft.Extensions.Configuration.CommandLine
#endregion /////////////////////////////////////////////////////////////////////////////////////////

namespace App.Client.Console
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Application Settings
            var appSettings = new AppSettings();
            Configuration.Bind("AppSettings", appSettings);
            services.AddSingleton(appSettings);

            // Logging Dependency
            services.AddLogging(configure => configure.AddSerilog(new LoggerConfiguration().WriteTo.Console()
                                                                                                   .CreateLogger()));

            services.AddLogging(configure => configure.AddSerilog(new LoggerConfiguration().WriteTo.File(appSettings.LogFilePath)
                                                                                                   .MinimumLevel
                                                                                                   .Information()
                                                                                                   .CreateLogger()));
            // Git Repository Dependency
            services.AddTransient<IGitRepository, GitRepository>();
            services.AddTransient<GitFileExporter>();

            // Json Placeholder Repository Dependency
            // services.AddTransient<IJsonPlaceholderRepository, JsonPlaceholderRepository>();
            // services.AddTransient<GitFileExporter>();
        }
    }
}