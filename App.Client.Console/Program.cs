using App.Core.Application;
using App.Core.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace App.Client.Console
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup(args);
            startup.ConfigureServices(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            // test it
            ILogger<Program> logger = serviceProvider.GetService<ILogger<Program>>();
            logger.LogInformation($"Hello World {System.DateTime.Now}");

            // setting
            AppSettings setting = serviceProvider.GetService<AppSettings>();
            logger.LogInformation($"User name is {setting.UserName} Password is {setting.Password}");

            // GIT Export request
            GitFileExporter git = serviceProvider.GetService<GitFileExporter>();
            await git.ExecuteAsync();

            // Json Placeholder Export request
            JsonPlaceholderFileExporter json = serviceProvider.GetService<JsonPlaceholderFileExporter>();
            await json.ExecuteAsync();
        }
    }
}