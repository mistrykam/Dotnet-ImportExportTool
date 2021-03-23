using App.Core.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace App.Client.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup(args);
            startup.ConfigureServices(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            // test it
            var logger = serviceProvider.GetService<ILogger<Program>>();
            logger.LogInformation($"Hello World {System.DateTime.Now}");

            // setting
            var setting = serviceProvider.GetService<AppSettings>();
            logger.LogInformation($"User name is {setting.UserName} Password is {setting.Password}");
        }
    }
}