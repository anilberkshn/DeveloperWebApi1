using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.Middleware;

namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureAppConfiguration(
                    (_, config) =>// host builder context olmadığı için _ kullanıldı. 
                { 
                    config.AddJsonFile("configuration.json");
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>()
                    .Configure(app => app.UseOcelot()); });
    }
}