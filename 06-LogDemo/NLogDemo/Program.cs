using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using NLog;
using NLog.Config;
using NLog.Web;

namespace NLogDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                {
                    var logFactory = new LogFactory();
                    LoggingConfiguration configuration = new LoggingConfiguration();
                    NLog.Web.NLogBuilder.ConfigureNLog(configuration);
                }

                var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
                logger.Info("Start...");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(configureLogging =>
                {
                    configureLogging.ClearProviders();
                    configureLogging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog();
    }
}
