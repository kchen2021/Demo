using System;
using System.Collections.Generic;
using System.Text;

namespace xApplicationInsights.Monitoring.Core
{
    public class MonitorKeyNotFoundException : Exception
    {
        public MonitorKeyNotFoundException(
            string message,
            Exception innerException
        ) : base(message, innerException)
        { }
    }
}