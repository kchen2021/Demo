using System;

namespace QuartzDemo.Models
{
    public class JobMetaData
    {
        public string JobId { get; set; }
        public Type JobType { get; }
        public string JobName { get; }
        public string CronExpression { get; }

        public JobMetaData(string id, Type jobType, string jobName, string cronExpression)
        {
            JobId = string.IsNullOrWhiteSpace(id) ? Guid.NewGuid().ToString() : id;
            JobType = jobType;
            JobName = jobName;
            CronExpression = cronExpression;
        }
    }
}
