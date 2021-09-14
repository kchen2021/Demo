using System;
using Quartz;
using Quartz.Spi;

namespace QuartzDemo.Services.JobFactory
{
    public class CustomQuartzJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public CustomQuartzJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IJob NewJob(TriggerFiredBundle triggerFiredBundle, IScheduler scheduler)
        {
            var jobDetail = triggerFiredBundle.JobDetail;
            return (IJob)_serviceProvider.GetService(jobDetail.JobType);
        }
        public void ReturnJob(IJob job) { }
    }
}
