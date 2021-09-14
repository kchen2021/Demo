using System;
using System.Threading;
using xApplicationInsights.Monitoring.Interfaces;

namespace ConfluentKafka.Core.Extentions
{
    #region ProcessorBase
    /// <summary>
    /// Base class for repetitive actions, which should be run on 
    /// a dedicated thread continuously. It provides functionality 
    /// to pause and resume. 
    /// </summary>
    public abstract class ProcessorBase : IProcessorNotifier, IDisposable
    {
        protected ProcessorBase(IMonitorFactory monitorFactory)
        {
            //if (log == null) throw new ArgumentNullException(nameof(log));
            if (monitorFactory == null) throw new ArgumentNullException(nameof(monitorFactory));
            //_log = log;
            _monitorFactoryLifeScope = monitorFactory.BeginLifetimeScope();
            _monitorThreadState = _monitorFactoryLifeScope.Create("_ThreadState", "not-started");
            _monitorThreadUnhandledException = _monitorFactoryLifeScope.Create<Exception>("_ThreadUnhandledException", null);

            _thread = new Thread(Process)
            {
                Name = GetType().Name,
                IsBackground = true
            };

            _thread.Start();
        }

        //private readonly IDataProcessingLog _log;
        private readonly Thread _thread;
        private readonly IMonitoree<string> _monitorThreadState;
        private readonly IMonitoree<Exception> _monitorThreadUnhandledException;
        private readonly object _lock = new object();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly IMonitorFactoryLifetimeScope _monitorFactoryLifeScope;

        private bool _disposed;
        private bool _running;

        public event Action ProcessStarted;
        public event Action ProcessStopped;
        public event Action ProcessTerminated;

        /// <summary>
        /// It's used for logging.
        /// </summary>
        protected abstract string Name { get; }

        /// <summary>
        /// Time out for the thread join during the dispose
        /// before calling the abort.
        /// </summary>
        protected abstract TimeSpan JoinTimeOut { get; }

        public void Start()
        {
            OnStart();
        }

        public void Stop()
        {
            OnStop();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// This action will be called continuously on a dedicated
        /// thread. It can be paused or resumed using start and 
        /// methods,
        /// </summary>
        /// <param name="token"></param>
        protected abstract void OnAction(CancellationToken token);

        protected virtual void OnStart()
        {
            Unblock();
            OnProcessStarted();
        }

        protected virtual void OnStop()
        {
            lock (_lock)
            {
                _running = false;
            }

            OnProcessStopped();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _disposed = true;

            _cancellationTokenSource.Cancel();

            if (disposing)
            {
                Unblock();

                if (_thread.IsAlive)
                    if (!_thread.Join(JoinTimeOut))
                        _thread.Abort();

                _monitorFactoryLifeScope?.Dispose();
            }

            //_log.Info($"{Name} disposed.");

            GC.SuppressFinalize(this);
        }

        private void Unblock()
        {
            lock (_lock)
            {
                _running = true;
                Monitor.Pulse(_lock);
            }
        }

        protected virtual void Process()
        {
            try
            {
                //_log.Info($"{Name} processor started.");
                Console.WriteLine($"{Name} processor started.");
                _monitorThreadState.SetValue("running");

                OnProcess();

                //_log.Info($"{Name} terminated.");
                Console.WriteLine($"{Name} terminated.");
                _monitorThreadState.SetValue("terminated-disposed");
            }
            catch (OperationCanceledException)
            {
                //_log.Info($"{Name} processor main thread cancellation requested.");
                Console.WriteLine($"{Name} processor main thread cancellation requested.");
                _monitorThreadState.SetValue("terminated-cancellation");
            }
            catch (ThreadAbortException)
            {
                //_log.Info($"{Name} processor main thread has been aborted.");
                Console.WriteLine($"{Name} processor main thread has been aborted.");
                _monitorThreadState.SetValue("terminated-aborted");
            }
            catch (Exception ex)
            {
                //_log.Fatal($"{Name} terminated with an exception", ex);
                _monitorThreadState.SetValue("terminated-exception");
                _monitorThreadUnhandledException.SetValue(ex);
            }
            finally
            {
                OnProcessStopped();
                OnProcessTerminated();
            }
        }

        private void OnProcess()
        {
            while (!_disposed)
            {
                BlockWhileInStopMode();

                if (_cancellationTokenSource.IsCancellationRequested)
                    break;

                OnAction(_cancellationTokenSource.Token);
            }
        }

        private void BlockWhileInStopMode()
        {
            if (_running) return;

            lock (_lock)
            {
                while (!_running)
                {
                    _monitorThreadState.SetValue("paused");
                    Monitor.Wait(_lock);
                    _monitorThreadState.SetValue("running");
                }
            }
        }

        protected void OnProcessStarted() =>
            ProcessStarted?.Invoke();

        protected void OnProcessStopped() =>
            ProcessStopped?.Invoke();

        protected void OnProcessTerminated() =>
            ProcessTerminated?.Invoke();
    }

    public interface IProcessorNotifier : IProcessor, INotifyProcessorStatusChanged
    {
    }

    public interface IProcessor
    {
        void Start();

        void Stop();
    }
    public interface INotifyProcessorStatusChanged
    {
        event Action ProcessStarted;

        event Action ProcessStopped;

        event Action ProcessTerminated;
    }
    #endregion
}
