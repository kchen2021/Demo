using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;
using QuartzDemo.Models;

namespace QuartzDemo.Services.JobFactory
{
    public class CustomQuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly JobMetaData _jobMetadata;
        private readonly ILogger<NotificationJob> _logger;
        public IScheduler Scheduler { get; set; }

        public CustomQuartzHostedService(
            ISchedulerFactory schedulerFactory,
            JobMetaData jobMetadata, 
            IJobFactory jobFactory,
            ILogger<NotificationJob> logger)
        {
            _schedulerFactory = schedulerFactory;
            _jobMetadata = jobMetadata;
            _jobFactory = jobFactory;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //Scheduler = await new StdSchedulerFactory().GetScheduler(cancellationToken);
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);//推荐使用这个
            Scheduler.JobFactory = _jobFactory;
            var job = CreateJob();
            var trigger = CreateTrigger();
            await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler.Shutdown(cancellationToken);
        }

        private ITrigger CreateTrigger()
        {
            return TriggerBuilder.Create()
                .WithIdentity(_jobMetadata.JobId)
                .WithCronSchedule(_jobMetadata.CronExpression)
                .WithDescription(_jobMetadata.JobName)
                .Build();
        }

        private IJobDetail CreateJob()
        {
            return JobBuilder
                .Create(_jobMetadata.JobType)
                .WithIdentity(_jobMetadata.JobId)
                .WithDescription(_jobMetadata.JobName)
                .Build();
        }
    }
}
