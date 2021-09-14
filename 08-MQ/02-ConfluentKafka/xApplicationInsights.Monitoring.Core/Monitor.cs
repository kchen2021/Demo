using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using xApplicationInsights.Monitoring.Interfaces;

namespace xApplicationInsights.Monitoring.Core
{
    /// <summary>
    /// An implementation of IMonitor and IMonitoree. For more details check the
    /// comments on those interface.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public sealed class Monitor<TValue> : IMonitor, IMonitoree<TValue>
    {
        public Monitor(
            string key,
            TValue initialValue,
            IReadOnlyDictionary<string, object> properties,
            IDisposable subscription
        )
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (subscription == null) throw new ArgumentNullException(nameof(subscription));
            if (properties == null) throw new ArgumentNullException(nameof(properties));

            _key = key;
            _subscription = subscription;
            _properties = properties;
            _initialValue = initialValue;
            OnReset();
        }

        private readonly string _key;
        private readonly IDisposable _subscription;
        private readonly IReadOnlyDictionary<string, object> _properties;
        private readonly TValue _initialValue;
        private TValue _value;
        private long _version;
        private bool _disposed;

        void IMonitoree<TValue>.SetValue(TValue value)
        {
            OnSetValue(value);
        }
        string IMonitor.Key => _key;
        long IMonitor.Version => _version;
        object IMonitor.Value => _value;
        void IMonitor.Reset() => OnReset();

        public bool TryGetProperty(string name, out object value) =>
            _properties.TryGetValue(name, out value);

        private void OnReset() => OnSetValue(_initialValue);
        private void OnSetValue(TValue value)
        {
            _value = value;
            Interlocked.Increment(ref _version);
        }

        public TValue ModifyValue(Func<TValue, TValue> modifier)
        {
            var value = modifier(_value);
            OnSetValue(value);
            return value;
        }

        #region IDisposable Support

        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            _disposed = true;

            if (disposing)
                _subscription.Dispose();
        }

        ~Monitor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}