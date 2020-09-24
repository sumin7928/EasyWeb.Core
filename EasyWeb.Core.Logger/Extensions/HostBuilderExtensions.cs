using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyWeb.Core.Logger.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureEasyWebLogger(this IHostBuilder builder)
        {
            if (EasyWebLogger.IsUseNLog)
            {
                builder.ConfigureLogging((hostContext, logger) =>
                {
                    logger.ClearProviders();
                    logger.SetMinimumLevel(LogLevel.Information);
                });
                builder.UseNLog();
            }
            else if(EasyWebLogger.IsUseSerilog)
            {
                builder.UseSerilog();
            }

            return builder;
        }

    }
}
