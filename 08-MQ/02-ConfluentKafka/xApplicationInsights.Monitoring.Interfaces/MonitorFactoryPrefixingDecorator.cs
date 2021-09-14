using System;
using System.Collections.Generic;
using System.Text;

namespace xApplicationInsights.Monitoring.Interfaces
{

    /// <summary>
    /// This is a monitor factory, which decorates an other factory to add constant sub prefixes to
    /// the monitor keys.
    /// </summary>
    public class MonitorFactoryPrefixingDecorator : IMonitorFactory
    {
        public MonitorFactoryPrefixingDecorator(IMonitorFactory decoratedMonitorFactory, string subPrefix)
        {
            if (decoratedMonitorFactory == null) throw new ArgumentNullException(nameof(decoratedMonitorFactory));
            if (subPrefix == null) throw new ArgumentNullException(nameof(subPrefix));

            _decoratedMonitorFactory = decoratedMonitorFactory;
            _subPrefix = subPrefix;
        }

        private readonly IMonitorFactory _decoratedMonitorFactory;
        private readonly string _subPrefix;

        public IMonitoree<TValue> Create<TValue>(string key, TValue initialValue) =>
            _decoratedMonitorFactory.Create(
                $"{_subPrefix}{key}",
                initialValue
            );

        public IMonitoree<TValue> Create<TValue>(
            string key,
            TValue initialValue,
            IReadOnlyDictionary<string, object> properties
        ) => _decoratedMonitorFactory.Create(
            $"{_subPrefix}{key}",
            initialValue,
            properties
        );
    }
}
