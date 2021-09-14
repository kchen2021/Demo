using System;
using System.Collections.Generic;
using System.Text;

namespace xApplicationInsights.Monitoring.Interfaces
{
    /// <summary>
    /// Repository of monitors
    /// </summary>
    public interface IMonitorRepository
    {
        int Count { get; }
        long Revision { get; }
        IEnumerable<IMonitor> GetAll();
        IMonitor Get(string name);
    }
}