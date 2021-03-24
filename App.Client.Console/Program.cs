using App.Core.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace App.Client.Console
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // startup configuration
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup(args);
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            // GIT Export request
            GitFileExporter git = serviceProvider.GetService<GitFileExporter>();
            await git.ExecuteAsync();

            // Json Placeholder Export request
            JsonPlaceholderFileExporter json = serviceProvider.GetService<JsonPlaceholderFileExporter>();
            await json.ExecuteAsync();
        }
    }
}