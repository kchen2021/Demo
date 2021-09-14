using System;
using System.Collections.Generic;
using System.Text;

namespace xApplicationInsights.Monitoring.Interfaces
{
    public static class MonitorFactoryExtensions
    {
        /// <summary>
        /// Creates a wrapper around the decorated factory, which adds
        /// the specified prefix to all monitors created using this 
        /// decorator.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="subPrefix"></param>
        /// <returns></returns>
        public static IMonitorFactory DecorateWithSubPrefixer(this IMonitorFactory factory, string subPrefix)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (subPrefix == null) throw new ArgumentNullException(nameof(subPrefix));
            return new MonitorFactoryPrefixingDecorator(factory, subPrefix);
        }
        /// <summary>
        /// Creates a wrapper around the provided factory. All of the 
        /// monitors created using the wrapper will be disposed when the 
        /// wrapper is disposed (if they were not already garbage collected).
        /// Note: The wrapper keeps a weak-reference to the created monitors
        /// to prevent memory leaks and provide possibility for having 
        /// nested life time scopes.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IMonitorFactoryLifetimeScope BeginLifetimeScope(this IMonitorFactory factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            return new MonitorFactoryLifetimeScopeDecorator(factory);
        }
        /// <summary>
        /// A shortcut to create a monitor of type long.
        /// </summary>
        /// <param name="monitorFactory"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IMonitoree<long> CreateCounter(
            this IMonitorFactory monitorFactory,
            string key
        ) => monitorFactory.Create<long>(key, 0);
    }
}
