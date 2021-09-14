using System;
using System.Collections.Generic;
using System.Linq;
using Confluent.Kafka;

namespace ConfluentKafka.Core.xKafkaConfluent
{
    public class ConfluentConsumerStatisticsReportGenerator<TKey, TValue>
    {
        public ConfluentConsumerStatisticsReportGenerator(
            ConsumerStatistics<TKey, TValue> consumerStatistics
        )
        {
            if (consumerStatistics == null) throw new ArgumentNullException(nameof(consumerStatistics));
            _consumerStatistics = consumerStatistics;
        }

        private readonly ConsumerStatistics<TKey, TValue> _consumerStatistics;

        public string ConfluentConsumerStatistics
            => _consumerStatistics.ConfluentConsumerStatistics;
        public object LastSuccessfulCommittedOffsets => _consumerStatistics
            .LastSuccessfulCommittedOffsets
            ?.Offsets
            ?.Select(o => new { o.Partition, Offset = o.Offset.Value });
        public object LastFailedCommittedOffsets => _consumerStatistics
            .LastFailedCommittedOffsets
            ?.Offsets
            ?.Select(o => new { o.Partition, Offset = o.Offset.Value, o.Error });
        public long SuccessfulCommittedOffsetsCount => _consumerStatistics.SuccessfulCommittedOffsetsCount;
        public long FailedCommittedOffsetsCount => _consumerStatistics.FailedCommittedOffsetsCount;
        public object PartitionsAssigned => _consumerStatistics
            .PartitionsAssigned
            ?.Select(p => p.Partition);
        public string GroupMemberId => _consumerStatistics.GroupMemberId;
        public Error LastGeneralError => _consumerStatistics.LastGeneralError;
        public long GeneralErrorsCount => _consumerStatistics.GeneralErrorsCount;
        public Message ConsumptionError => _consumerStatistics.LastConsumptionError;
        public long ConsumptionErrorsCount => _consumerStatistics.ConsumptionErrorsCount;
        public Message<TKey, TValue> LastMessageReceived => _consumerStatistics.LastMessageReceived;
        public IReadOnlyDictionary<string, object> ConsumerConfigs => _consumerStatistics.ConsumerConfigs;
    }
}
