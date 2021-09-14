using System;
using System.Collections.Generic;
using System.Text;

namespace xApplicationInsights.Monitoring.Core
{
    public class MonitorKeyDuplicationException : Exception
    {
        public MonitorKeyDuplicationException(string message) : base(message)
        { }
    }
}