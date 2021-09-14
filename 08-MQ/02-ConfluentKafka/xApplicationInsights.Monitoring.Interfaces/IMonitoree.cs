using System;
using System.Collections.Generic;
using System.Text;

namespace xApplicationInsights.Monitoring.Interfaces
{
    /// <summary>
    /// This interface is to provide information about the internal state to the monitor
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface IMonitoree<TValue> : IDisposable
    {
        void SetValue(TValue value);
        TValue ModifyValue(Func<TValue, TValue> value);
    }
}