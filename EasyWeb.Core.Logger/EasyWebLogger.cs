using EasyWeb.Core.Logger.Configurations;
using NLog.Web;
using System;
using System.IO;

namespace EasyWeb.Core.Logger
{
    public static class EasyWebLogger
    {
        public static bool IsUseNLog { get; set; } = false;
        public static bool IsUseSerilog { get; set; } = false;

        public static void CreateNLogLogger()
        {
            if (IsUseSerilog)
            {
                throw new InvalidOperationException("Duplicated EasyWeb.Core Logger - Now use serilog");
            }

            NLogConfiguration.Initialize(GetEnvironment());

            IsUseNLog = true;
        }

        public static void CreateSerilogLogger()
        {
            if (IsUseNLog)
            {
                throw new InvalidOperationException("Duplicated EasyWeb.Core Logger - Now use nlog");
            }

            SerilogConfiguration.Initialize(GetEnvironment());

            IsUseSerilog = true;
        }

        public static void Shutdown()
        {
            if (IsUseNLog)
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
            if (IsUseSerilog)
            {
                Serilog.Log.CloseAndFlush();
            }
        }

        private static string GetEnvironment()
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == null || env == string.Empty)
            {
                throw new InvalidOperationException("Not found [ASPNETCORE_ENVIRONMENT] environment value");
            }

            return env;
        }
    }
}
