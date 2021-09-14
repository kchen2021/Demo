using System;

namespace ConfluentKafa.Interfaces
{
    public struct Message<TKey, TValue>
    {
        public Message(TKey key, TValue value, DateTime timestamp)
        {
            Key = key;
            Value = value;
            Timestamp = timestamp;
        }

        /// <summary>
        /// This property represents message creation or log append time stamp. 
        /// Depending on log.message.timestamp.type server setting it can
        /// represent CreateTime or LogAppendTime. The default is CreationTime.
        /// </summary>
        public DateTime Timestamp { get; }
        public TKey Key { get; }
        public TValue Value { get; }

        public override string ToString() =>
            $"{{\"Key\": \"{Key}\", \"Value\": \"{Value}\", \"Time-stamp\": \"{Timestamp}\"}}";
    }
}
