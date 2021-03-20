using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;

namespace e_me.Core.Logging
{
    public class ApplicationLoggerFactory
    {
        private static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public static ILoggerFactory Create(IConfiguration configuration)
        {
            Log.Logger = CreateLogger(configuration);

            return new LoggerFactory()
                .AddSerilog();
        }

        private static Logger CreateLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .WriteTo.File(LogFilePath, rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer(configuration.GetConnectionString("DbConnection"),
                    new MSSqlServerSinkOptions { TableName = "Log", AutoCreateSqlTable = true })
                .CreateLogger();
        }

        private static string LogFilePath =>
            Path.Combine(BaseDirectory, "Logs", "Log.txt");
    }
}
