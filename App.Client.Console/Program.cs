using App.Core.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace App.Client.Console
{
    /// <summary>
    /// Console Client Application
    /// Call the Startup class to configure the application before executing import/export steps
    /// </summary>
    internal class Program
    {
        private static async Task<int> Main(string[] args)
        {
            try
            {
                // startup configuration
                IServiceCollection services = new ServiceCollection();
                Startup startup = new Startup(args);
                startup.ConfigureServices(services);
                IServiceProvider serviceProvider = services.BuildServiceProvider();

                // TO DO: Add commandline parsing
                // Command line parsing: https://github.com/commandlineparser/commandline
                // dotnet add package CommandLineParser --version 2.8.0

                // GIT Export request
                GitFileExporter git = serviceProvider.GetService<GitFileExporter>();
                await git.ExecuteAsync();

                // Json Placeholder Export request
                JsonPlaceholderFileExporter json = serviceProvider.GetService<JsonPlaceholderFileExporter>();
                await json.ExecuteAsync();

                // exited normally
                return 0;
            }
            catch (Exception ex)
            {
                // Report the exception and return -1 to indicate the failure
                System.Console.WriteLine($"Fatal Error: {ex.Message}");

                return -1;
            }
        }
    }
}