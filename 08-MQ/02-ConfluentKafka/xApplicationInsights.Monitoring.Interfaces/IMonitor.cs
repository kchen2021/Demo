using System;
using System.Collections.Generic;
using System.Text;

namespace xApplicationInsights.Monitoring.Interfaces
{
    /// <summary>
    /// This interface is used to monitor changes to the internal state of
    /// different classes
    /// </summary>
    public interface IMonitor
    {
        string Key { get; }
        object Value { get; }
        long Version { get; }
        void Reset();
        bool TryGetProperty(string name, out object value);
    }
}