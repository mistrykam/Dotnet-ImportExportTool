using App.Core.Application;
using App.Core.Domain.Entities;
using App.Core.Domain.Repository;
using App.Infrastructure.DataAccess;
using App.Infrastructure.ExportData;
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
// Configuration Settings
// > dotnet add package Microsoft.Extensions.Configuration
// > dotnet add package Microsoft.Extensions.Configuration.Binder
// > dotnet add package Microsoft.Extensions.Configuration.Json
// > dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables
// > dotnet add package Microsoft.Extensions.Configuration.CommandLine
//
// Logging
// > dotnet add package Microsoft.Extensions.Logging
//
// Serilog
// > dotnet add package Serilog
// > dotnet add package Serilog.Extensions.Hosting
// > dotnet add package Serilog.Settings.Configuration
// > dotnet add package Serilog.Sinks.File
// > dotnet add package Serilog.Sinks.Console
//
// Splunk Logging
// > dotnet add package Serilog.Sinks.Splunk
//
#endregion /////////////////////////////////////////////////////////////////////////////////////////

namespace App.Client.Console
{
    /// <summary>
    /// Startup will read the AppSetting.json file and setup the dependency injection framework.
    /// 
    /// Uses:
    ///     Microsoft.Extensions.DependencyInjection
    ///     Microsoft.Extensions.Logging
    ///     Microsoft.Extensions.Configuration
    ///     Serilog
    ///     
    ///     
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                                                .SetBasePath(Directory.GetCurrentDirectory())
                                                // load in the json file as configuration
                                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                // load environment variables
                                                .AddEnvironmentVariables()
                                                // override configuration from the json file with commandline arguments
                                                .AddCommandLine(args);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Application Settings
            var appSettings = new AppSettings();
            Configuration.Bind("AppSettings", appSettings);
            services.AddSingleton(appSettings);

            // Logging Dependency (https://github.com/serilog/serilog-settings-configuration)
            services.AddLogging(configure => configure.AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(Configuration)                                                                                           
                                                                                           .CreateLogger()));

            // Git dependencies
            services.AddTransient<IGitRepository, GitRepository>();
            services.AddTransient<GitImportExport>();

            // Json Placeholder dependencies
            services.AddTransient<IJsonPlaceholderRepository, JsonPlaceholderRepository>();
            services.AddTransient<JsonPlaceholderImportExport>();

            // File Export dependencies
            services.AddTransient<IFileExportRepository, FileExportRepository>();
        }
    }
}