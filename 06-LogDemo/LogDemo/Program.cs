using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

namespace LogDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(hostBuilder =>
                {
                    var eventLogSettings = new EventLogSettings();
                    eventLogSettings.LogName = "LogName_LogDemo";
                    eventLogSettings.MachineName = "MachineName_LogDemo";
                    eventLogSettings.SourceName = "SourceName_LogName";
                    eventLogSettings.Filter = (s, level) =>
                    {
                        if (s.Contains("error"))
                        {
                            return true;
                        }

                        return false;
                    };

                    hostBuilder.AddEventLog(eventLogSettings);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
