using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyWeb.Core.Logger;
using EasyWeb.Core.Logger.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EasyWeb.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                EasyWebLogger.CreateNLogLogger();
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to run web server - message:{e.Message}");
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                EasyWebLogger.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureEasyWebLogger()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
