using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasyWeb.Core.Logger.Configurations
{
    public static class SerilogConfiguration
    {
        public static void Initialize(string env)
        {
            var appConfig = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                    .Build();

            LoggerConfiguration loggerConfig = new LoggerConfiguration();
            appConfig.Bind("EasyWebCore:Logger", loggerConfig);

            Log.Logger = new Serilog.LoggerConfiguration()
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .WriteTo.File(
                        $"{loggerConfig.LogPath}/{loggerConfig.FileName}-.log",
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        retainedFileCountLimit: loggerConfig.MaxArchiveFiles)
                    .CreateLogger();

            Log.Information("test log");
        }
    }
}
