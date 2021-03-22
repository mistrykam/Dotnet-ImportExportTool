using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System;

namespace App.Client.Console
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddSerilog(new LoggerConfiguration().WriteTo.Console().CreateLogger()));
            services.AddLogging(configure => configure.AddSerilog(new LoggerConfiguration().WriteTo.File("log.txt").MinimumLevel.Information().CreateLogger()));
        }
    }
}
