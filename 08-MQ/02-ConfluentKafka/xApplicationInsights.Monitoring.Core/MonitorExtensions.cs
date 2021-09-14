using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using xApplicationInsights.Monitoring.Interfaces;

namespace xApplicationInsights.Monitoring.Core
{
    public static class MonitorExtensions
    {
        private class MonitorModel
        {
            public string Key { get; set; }
            public object Value { get; set; }
            public long Version { get; set; }
        }

        public static string ToJsonAll(this IEnumerable<IMonitor> monitors) =>
            JsonConvert.SerializeObject(monitors.Select(ToMonitorModel));

        private static MonitorModel ToMonitorModel(IMonitor monitor) =>
            monitor == null
                ? new MonitorModel()
                : new MonitorModel
                {
                    Key = monitor.Key,
                    Value = monitor.Value,
                    Version = monitor.Version
                };
    }
}
