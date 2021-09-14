using System;
using System.Collections.Generic;
using System.Text;

namespace xApplicationInsights.Monitoring.Interfaces
{
    /// <summary>
    /// Factory interface for creating monitors
    /// </summary>
    public interface IMonitorFactory
    {
        /// <summary>
        /// Creates a monitor.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="key">The name of the monitor.</param>
        /// <param name="initialValue">The initial value set to the monitor
        /// at creation and upon each call to the Reset.</param>
        /// <returns></returns>
        IMonitoree<TValue> Create<TValue>(string key, TValue initialValue);
        /// <summary>
        /// Creates a monitor.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="key">The name of the monitor.</param>
        /// <param name="initialValue">The initial value set to the monitor
        /// at creation and upon each call to the Reset.</param>
        /// <param name="properties">
        /// Properties assigned to the monitor to describe it. 
        /// This value could be used to request for auto reset, create
        /// performance counters, and so on.
        /// </param>
        /// <returns></returns>
        IMonitoree<TValue> Create<TValue>(
            string key,
            TValue initialValue,
            IReadOnlyDictionary<string, object> properties
        );
    }
}