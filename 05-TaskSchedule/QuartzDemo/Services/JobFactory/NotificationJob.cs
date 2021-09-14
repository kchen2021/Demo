using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace QuartzDemo.Services.JobFactory
{
    [DisallowConcurrentExecution]//最多在同一时间只能执行一个
    public class NotificationJob : IJob
    {
        private readonly ILogger<NotificationJob> _logger;

        public NotificationJob(ILogger<NotificationJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            //Thread.Sleep(2000);
            _logger.LogInformation("Hello world!" + DateTime.Now);
            return Task.CompletedTask;
        }
    }
}