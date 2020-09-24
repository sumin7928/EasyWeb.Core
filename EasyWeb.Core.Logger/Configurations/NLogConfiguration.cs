using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasyWeb.Core.Logger.Configurations
{
    public static class NLogConfiguration
    {
        public static void Initialize(string env)
        {
            var appConfig = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                    .Build();

            LoggerConfiguration loggerConfig = new LoggerConfiguration();
            appConfig.Bind("EasyWebCore:Logger", loggerConfig);

            var nlogConfig = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            string numberingFormat = "{########}";
            var allLogFiles = new NLog.Targets.FileTarget("allLogFiles")
            {
                FileName = $"{loggerConfig.LogPath}/{loggerConfig.FileName}.log",
                ArchiveEvery = NLog.Targets.FileArchivePeriod.Day,
                ArchiveFileName = $"{loggerConfig.LogPath}/back/{loggerConfig.FileName}-{numberingFormat}.log",
                ArchiveNumbering = NLog.Targets.ArchiveNumberingMode.Date,
                ArchiveDateFormat = "yyyyMMdd",
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss.fff} [${threadid}] ${level:uppercase=true} ${logger:shortName=true} - ${message} ${exception:format=tostring}",
                Encoding = Encoding.UTF8,
                MaxArchiveFiles = loggerConfig.MaxArchiveFiles
            };

            var logconsole = new NLog.Targets.ConsoleTarget("logconsole") 
            {
                Layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss.fff} [${threadid}] ${level:uppercase=true} ${logger:shortName=true} - ${message} ${exception:format=tostring}"
            };

            // Rules for mapping loggers to targets            
            nlogConfig.AddRule(LogLevel.Trace, LogLevel.Fatal, allLogFiles);
            nlogConfig.AddRule(LogLevel.Trace, LogLevel.Fatal, logconsole);

            // Apply config           
            LogManager.Configuration = nlogConfig;
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(LogManager.Configuration).GetCurrentClassLogger();
            logger.Info("test log -- apply nlog");
        }
    }
}
