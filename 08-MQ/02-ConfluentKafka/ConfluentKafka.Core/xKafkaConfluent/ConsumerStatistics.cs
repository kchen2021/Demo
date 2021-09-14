using System.Collections.Generic;
using System.Linq;
using Confluent.Kafka;

namespace ConfluentKafka.Core.xKafkaConfluent
{
    #region ConsumerStatistics

    public class ConsumerStatistics<TKey, TValue>
    {
        private readonly HashSet<TopicPartition> _partitionsAssigned = new HashSet<TopicPartition>();
        private readonly object _lock = new object();

        public string ConfluentConsumerStatistics;
        public CommittedOffsets LastSuccessfulCommittedOffsets;
        public CommittedOffsets LastFailedCommittedOffsets;
        public long SuccessfulCommittedOffsetsCount;
        public long FailedCommittedOffsetsCount;
        public string GroupMemberId;
        public Error LastGeneralError;
        public long GeneralErrorsCount;
        public Message LastConsumptionError;
        public long ConsumptionErrorsCount;
        public Message<TKey, TValue> LastMessageReceived;
        public IReadOnlyDictionary<string, object> ConsumerConfigs;
        public IEnumerable<TopicPartition> PartitionsAssigned => GetAssignedPartitions();

        public IEnumerable<TopicPartition> GetAssignedPartitions()
        {
            lock (_lock)
            {
                return _partitionsAssigned.ToArray();
            }
        }

        public void AddRangeToAssignedPartitions(IEnumerable<TopicPartition> topicPartitions)
        {
            lock (_lock)
            {
                foreach (var topicPartition in topicPartitions)
                {
                    _partitionsAssigned.Add(topicPartition);
                }
            }
        }

        public void RemoveRangeFromAssignedPartitions(IEnumerable<TopicPartition> topicPartitions)
        {
            lock (_lock)
            {
                foreach (var topicPartition in topicPartitions)
                {
                    _partitionsAssigned.Remove(topicPartition);
                }
            }
        }
    }

    #endregion
}
