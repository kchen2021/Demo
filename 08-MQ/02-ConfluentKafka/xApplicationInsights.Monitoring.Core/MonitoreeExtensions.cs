using xApplicationInsights.Monitoring.Interfaces;

namespace xApplicationInsights.Monitoring.Core
{
    public static class MonitoreeExtensions
    {
        public static long Inc(this IMonitoree<long> monitoree) =>
            monitoree.ModifyValue(value => value + 1);

        public static long Dec(this IMonitoree<long> monitoree) =>
            monitoree.ModifyValue(value => value - 1);

        public static long Reset(this IMonitoree<long> monitoree)
        {
            monitoree.SetValue(0);
            return 0;
        }
    }
}