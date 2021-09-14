using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using xApplicationInsights.Monitoring.Interfaces;

namespace xApplicationInsights.Monitoring.Core
{
    /// <summary>
    /// IMonitorFactory and IMonitorRepository implementation. It keeps a 
    /// weak-reference to each monitor object and makes sure there is no  
    /// duplication in monitor keys. The weak-reference is to prevent memory 
    /// leaks and let the monitoree decide about the life cycle of the 
    /// monitor object. Once the monitor object is disposed, it will be 
    /// removed from the repository automatically.
    /// For more details check comments on the interfaces.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class MonitorFactory : IMonitorFactory, IMonitorRepository
    {
        private class MonitorSubscription : IDisposable
        {
            public MonitorSubscription(string key, MonitorFactory factory)
            {
                _key = key;
                _factory = factory;
            }

            private readonly string _key;
            private readonly MonitorFactory _factory;

            public void Dispose()
            {
                try
                {
                    WeakReference<IMonitor> _;
                    if (_factory._monitors.TryRemove(_key, out _))
                    {
                        _factory._monitorNumberOfMonitors?.SetValue(
                            _factory._monitors.Count
                        );
                        Interlocked.Increment(ref _factory._revision);
                    }

                    _factory._monitorNumberOfGcedMonitors?.ModifyValue(i => i + 1);
                }
                catch
                {
                    //To prevent exceptions on GC thread, which can happen 
                    //in rare cases when the repository is GCed before the 
                    //monitor finalizer is called. This situation can only 
                    //happen if the whole object graph (repository and 
                    //monitors) is marked as eligible for collection
                    _factory._monitorNumberOfExceptions?.ModifyValue(i => i + 1);
                }
            }
        }

        public MonitorFactory(string prefix)
        {
            if (prefix == null) throw new ArgumentNullException(nameof(prefix));
            _prefix = prefix;

            _monitorNumberOfMonitors = Create("_MonitorFactory_NumberOfMonitors", 0);
            _monitorNumberOfGcedMonitors = Create<long>("_MonitorFactory_NumberOfGcedMonitors", 0);
            _monitorNumberOfExceptions = Create<long>("_MonitorFactory_NumberOfExceptions", 0);
        }

        private static readonly IReadOnlyDictionary<string, object> EmptySettings
            = new Dictionary<string, object>();

        private readonly IMonitoree<int> _monitorNumberOfMonitors;
        private readonly IMonitoree<long> _monitorNumberOfGcedMonitors;
        private readonly IMonitoree<long> _monitorNumberOfExceptions;
        private readonly string _prefix;
        private long _revision;

        private readonly ConcurrentDictionary<string, WeakReference<IMonitor>> _monitors
            = new ConcurrentDictionary<string, WeakReference<IMonitor>>();

        public int Count => _monitors.Count;
        public long Revision => _revision;

        public IMonitoree<TValue> Create<TValue>(string key, TValue initialValue) =>
            Create(key, initialValue, EmptySettings);

        public IMonitoree<TValue> Create<TValue>(
            string key,
            TValue initialValue,
            IReadOnlyDictionary<string, object> properties
        )
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            var fullKey = $"{_prefix}{key}";
            var monitor = new Monitor<TValue>(
                fullKey,
                initialValue,
                properties,
                new MonitorSubscription(fullKey, this)
            );

            if (!_monitors.TryAdd(fullKey, new WeakReference<IMonitor>(monitor)))
                throw new MonitorKeyDuplicationException(
                    $"A monitor with the same key ({fullKey}) already exists"
                );

            _monitorNumberOfMonitors?.SetValue(_monitors.Count);
            Interlocked.Increment(ref _revision);
            return monitor;

        }

        public IEnumerable<IMonitor> GetAll()
        {
            foreach (var monitorsWeakRef in _monitors.Values)
            {
                IMonitor monitor;

                if (monitorsWeakRef.TryGetTarget(out monitor))
                    yield return monitor;
            }
        }

        public IMonitor Get(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            WeakReference<IMonitor> monitorsWeakRef;
            IMonitor monitor;
            if (
                _monitors.TryGetValue(
                    name,
                    out monitorsWeakRef
                ) &&
                monitorsWeakRef.TryGetTarget(out monitor)
            )
                return monitor;

            throw new MonitorKeyNotFoundException($"Monitor not found for key '{name}'", null);
        }
    }
}

