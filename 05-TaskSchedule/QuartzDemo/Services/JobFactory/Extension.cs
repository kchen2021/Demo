using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using QuartzDemo.Models;

namespace QuartzDemo.Services.JobFactory
{
    public static class Extension
    {
        public static void AddLifeScope(this IServiceCollection services)
        {
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//quartz的函数注入

            services.AddSingleton<IJobFactory, CustomQuartzJobFactory>();
            services.AddSingleton<NotificationJob>();
            services.AddHostedService<CustomQuartzHostedService>();
            services.AddSingleton(new JobMetaData(Guid.NewGuid().ToString(), typeof(NotificationJob), "Notification Job", "0/1 * * * * ?"));
        }
    }
}