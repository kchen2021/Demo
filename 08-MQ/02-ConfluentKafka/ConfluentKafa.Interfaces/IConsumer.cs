using System;
using System.Threading.Tasks;
using ConfluentKafa.Interfaces.Errors;

namespace ConfluentKafa.Interfaces
{
    public interface IConsumer : IDisposable
    {
        /// <summary>
        /// Error in consumed message.
        /// </summary>
        event EventHandler<MessagingError> ErrorAccured;
        Task AcknowledgeAll();
        void Start();
        void Stop();
    }
    public interface IConsumer<TKey, TValue> : IConsumer
    {
        event EventHandler<Message<TKey, TValue>> MessageReceived;
    }
}
