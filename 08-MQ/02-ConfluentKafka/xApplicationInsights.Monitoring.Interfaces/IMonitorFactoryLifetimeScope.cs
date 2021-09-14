using System;
using System.Collections.Generic;
using System.Text;

namespace xApplicationInsights.Monitoring.Interfaces
{
    /// <summary>
    /// This is a factory with a life time associate with all 
    /// monitors create using it. If the 
    /// </summary>
    public interface IMonitorFactoryLifetimeScope : IMonitorFactory, IDisposable
    { }
}