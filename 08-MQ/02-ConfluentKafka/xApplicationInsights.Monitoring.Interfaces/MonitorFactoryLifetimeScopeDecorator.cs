using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace xApplicationInsights.Monitoring.Interfaces
{
    /// <summary>
    /// All of the monitors created using the decorator will be disposed 
    /// when the decorator is disposed (if they were not already garbage
    /// collected).
    /// Note: The decorator keeps a weak-reference to the created monitors
    /// to prevent memory leaks and provide possibility for having 
    /// nested life time scopes.
    /// </summary>
    public class MonitorFactoryLifetimeScopeDecorator : IMonitorFactoryLifetimeScope
    {
        public MonitorFactoryLifetimeScopeDecorator(IMonitorFactory decoratedFactory)
        {
            if (decoratedFactory == null) throw new ArgumentNullException(nameof(decoratedFactory));
            _decoratedFactory = decoratedFactory;
        }

        private readonly IMonitorFactory _decoratedFactory;
        private readonly ConcurrentQueue<WeakReference<IDisposable>> _moniotrs
            = new ConcurrentQueue<WeakReference<IDisposable>>();

        private bool _disposed;

        public IMonitoree<TValue> Create<TValue>(
            string key,
            TValue initialValue
        )
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(MonitorFactoryLifetimeScopeDecorator));

            var monitor = _decoratedFactory.Create(key, initialValue);
            _moniotrs.Enqueue(new WeakReference<IDisposable>(monitor));
            return monitor;
        }

        public IMonitoree<TValue> Create<TValue>(
            string key,
            TValue initialValue,
            IReadOnlyDictionary<string, object> properties
        )
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(MonitorFactoryLifetimeScopeDecorator));

            var monitor = _decoratedFactory.Create(key, initialValue, properties);
            _moniotrs.Enqueue(new WeakReference<IDisposable>(monitor));
            return monitor;
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            foreach (var item in _moniotrs)
            {
                IDisposable monitor;
                if (item.TryGetTarget(out monitor))
                    monitor.Dispose();
            }
        }
    }
}
