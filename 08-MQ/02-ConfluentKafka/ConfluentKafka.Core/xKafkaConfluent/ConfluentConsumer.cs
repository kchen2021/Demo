using Confluent.Kafka;
using ConfluentKafa.Interfaces;
using ConfluentKafa.Interfaces.Errors;
using ConfluentKafka.Core.Enums;
using ConfluentKafka.Core.Extentions;
using ConfluentKafka.Core.xSerialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ConfluentKafa.Interfaces.Extentions;
using xApplicationInsights.Monitoring.Interfaces;

namespace ConfluentKafka.Core.xKafkaConfluent
{
    public class ConfluentConsumer<TKey, TValue> : ProcessorBase, ConfluentKafa.Interfaces.IConsumer<TKey, TValue>
    {

        public ConfluentConsumer(
            string topicName,
            ISerializer<TKey> keySerializer,
            ISerializer<TValue> valueSerializer,
            IReadOnlyDictionary<string, object> configurations,
            IMonitorFactory monitoringFactory,
            StartPosition? startPosition = null
        ) : base(monitoringFactory)
        {
            if (topicName == null) throw new ArgumentNullException(nameof(topicName));
            if (valueSerializer == null) throw new ArgumentNullException(nameof(valueSerializer));
            if (keySerializer == null) throw new ArgumentNullException(nameof(keySerializer));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            if (monitoringFactory == null) throw new ArgumentNullException(nameof(monitoringFactory));
            //if (log == null) throw new ArgumentNullException(nameof(log));


            const string groupIdKey = "group.id";
            if (!configurations.ContainsKey(groupIdKey)) throw new KeyNotFoundException(groupIdKey);
            const string bootstrapServers = "bootstrap.servers";
            if (!configurations.ContainsKey(bootstrapServers)) throw new KeyNotFoundException(bootstrapServers);
            const string autoOffsetReset = "auto.offset.reset";
            if (!configurations.ContainsKey(autoOffsetReset)) throw new KeyNotFoundException(autoOffsetReset);

            _startPosition = startPosition;
            _consumerStatistics.ConsumerConfigs = configurations;
            _monitorConsumerStatistics = monitoringFactory.Create(
                "_ConsumerStatistics",
                new ConfluentConsumerStatisticsReportGenerator<TKey, TValue>(_consumerStatistics)
            );

            

            _consumer = new Confluent.Kafka.Consumer<TKey, TValue>(
                configurations,
                new ConfluentSerializerAdapter<TKey>(keySerializer),
                new ConfluentSerializerAdapter<TValue>(valueSerializer)
            );

            _consumer.OnMessage += ConfluentOnMessage;
            _consumer.OnPartitionEOF += ConfluentOnPartitionEof;
            _consumer.OnError += ConfluentOnError;
            _consumer.OnConsumeError += ConfluentOnConsumeError;
            _consumer.OnOffsetsCommitted += ConfluentOnOffsetsCommitted;
            _consumer.OnPartitionsAssigned += ConfluentOnPartitionsAssigned;
            _consumer.OnPartitionsRevoked += ConfluentOnPartitionsRevoked;
            _consumer.OnStatistics += ConfluentOnStatistics;
            _consumer.Subscribe(new[] { topicName });
            Name = topicName;

            JoinTimeOut = TimeSpan.FromSeconds(1);
            Console.WriteLine($"Subscribed to: [{string.Join(", ", _consumer.Subscription)}]");
        }











        private readonly ConsumerStatistics<TKey, TValue> _consumerStatistics = new ConsumerStatistics<TKey, TValue>();
        private readonly StartPosition? _startPosition;
        //private readonly IDataProcessingLog _log;
        private Confluent.Kafka.Consumer<TKey, TValue> _consumer;
        private readonly IMonitoree<ConfluentConsumerStatisticsReportGenerator<TKey, TValue>> _monitorConsumerStatistics;

        private bool _disposed;

        public event EventHandler<ConfluentKafa.Interfaces.Message<TKey, TValue>> MessageReceived;
        public event EventHandler<MessagingError> ErrorAccured;
        
        protected override string Name { get; }
        protected override TimeSpan JoinTimeOut { get; }

        public Task AcknowledgeAll()
        {
            return _consumer.CommitAsync();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_disposed)
                return;

            _disposed = true;

            if (!disposing)
                return;

            _consumer?.Dispose();
            _monitorConsumerStatistics?.Dispose();
        }
        protected override void OnAction(CancellationToken token)
        {
            _consumer.Poll(TimeSpan.FromMilliseconds(100));
        }
        private void OnMessageReceived(ConfluentKafa.Interfaces.Message<TKey, TValue> e)
        {
            
            MessageReceived?.Invoke(this, e);
        }
        private void OnErrorAccured(MessagingError e)
        {
            
            ErrorAccured?.Invoke(this, e);
        }
        private void ConfluentOnStatistics(object sender, string statistics)
        {
            
            Console.WriteLine($"{{\"LibrdKafkaStatistics\": {statistics}}}");
            _consumerStatistics.ConfluentConsumerStatistics = statistics;
        }
        private void ConfluentOnPartitionsRevoked(
            object sender,
            List<Confluent.Kafka.TopicPartition> topicPartitions
        )
        {
            
            var revoced = $"[{string.Join(", ", topicPartitions)}]";
            Console.WriteLine($"RevokedPartitions: {revoced}");
            _consumer.Unassign();
            _consumerStatistics.RemoveRangeFromAssignedPartitions(topicPartitions);
        }
        private void ConfluentOnPartitionsAssigned(
            object sender,
            List<Confluent.Kafka.TopicPartition> topicPartitions
        )
        {
            
            switch (_startPosition)
            {
                case StartPosition.End:
                    AssignTopicPartitionOffset(
                        topicPartitions,
                        Confluent.Kafka.Offset.End
                    );
                    break;
                case StartPosition.Beginning:
                    AssignTopicPartitionOffset(
                        topicPartitions,
                        Confluent.Kafka.Offset.Beginning
                    );
                    break;
                case StartPosition.Invalid:
                    AssignTopicPartitionOffset(
                        topicPartitions,
                        Confluent.Kafka.Offset.Invalid
                    );
                    break;
                case StartPosition.Stored:
                    AssignTopicPartitionOffset(
                        topicPartitions,
                        Confluent.Kafka.Offset.Stored
                    );
                    break;
                case null:
                    _consumer.Assign(topicPartitions);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var position = $"[{string.Join(", ", _consumer.Position(topicPartitions))}]";

            var partitions = $"[{string.Join(", ", topicPartitions)}]";

            string msg = $"Assigned partitions: {partitions}, " +
                         $"Starting Position Type: {_startPosition}, " +
                         $"Starting Position: {position}, " +
                         $"Member id: {_consumer.MemberId}";
            Console.WriteLine(msg);


            _consumerStatistics.AddRangeToAssignedPartitions(topicPartitions);
            _consumerStatistics.GroupMemberId = _consumer.MemberId;
        }

        private void AssignTopicPartitionOffset(
            IEnumerable<Confluent.Kafka.TopicPartition> topicPartitions,
            Confluent.Kafka.Offset offset
        )
        {
            
            _consumer.Assign(
                topicPartitions.Select(
                    i => new Confluent.Kafka.TopicPartitionOffset(i, offset)
                ));
        }


        private void ConfluentOnOffsetsCommitted(
            object sender,
            Confluent.Kafka.CommittedOffsets committedOffsets
        )
        {
            
            var offsets = $"[{string.Join(", ", committedOffsets.Offsets)}]";

            if (committedOffsets.Error)
            {
                var message = $"Error in committing offsets: {committedOffsets.Error}. Commit detail: {offsets}";

                Console.WriteLine(message);

                _consumerStatistics.LastFailedCommittedOffsets = committedOffsets;
                _consumerStatistics.FailedCommittedOffsetsCount++;

                OnErrorAccured(new MessagingConsumptionError(message));
            }
            else
            {
                _consumerStatistics.LastSuccessfulCommittedOffsets = committedOffsets;
                _consumerStatistics.SuccessfulCommittedOffsetsCount++;
            }
        }
        private void ConfluentOnConsumeError(object sender, Confluent.Kafka.Message message)
        {
            
            var topicPartition = $"{message.Topic}/{message.Partition}";
            var errorMessage = $"{{Time: {DateTime.UtcNow}, Offset: {message.Offset}, Error: {message.Error}}}";
            var loggedMessage = $"Error consuming from topic/partition {topicPartition}: {errorMessage}";
            Console.WriteLine(loggedMessage);

            _consumerStatistics.LastConsumptionError = message;
            _consumerStatistics.ConsumptionErrorsCount++;

            OnErrorAccured(new MessagingConsumptionError(loggedMessage));
        }
        private void ConfluentOnError(object sender, Confluent.Kafka.Error error)
        {
            
            var errorMessage = error.ToString();
            var loggedMessage = $"General error: {errorMessage}";
            Console.WriteLine(loggedMessage);

            _consumerStatistics.LastGeneralError = error;
            _consumerStatistics.GeneralErrorsCount++;

            OnErrorAccured(new MessagingGeneralError(loggedMessage));
        }
        private void ConfluentOnPartitionEof(
            object sender,
            TopicPartitionOffset topicPartitionOffset
        )
        {
            
            var topicPartition = $"{topicPartitionOffset.Topic}/{topicPartitionOffset.Partition}";
            
            string msg = $"Reached end of topic/partition {topicPartition}. " +
                         $"Next message will be at offset {topicPartitionOffset.Offset}";
            Console.WriteLine(msg);

        }
        private void ConfluentOnMessage(object sender, Confluent.Kafka.Message<TKey, TValue> message)
        {
            
            _consumerStatistics.LastMessageReceived = message;
            OnMessageReceived(
                new ConfluentKafa.Interfaces.Message<TKey, TValue>(
                    message.Key,
                    message.Value,
                    message.Timestamp.Type == TimestampType.NotAvailable 
                        ? DateTime.UtcNow 
                        : message.Timestamp.UtcDateTime
                )
            );
        }



        ~ConfluentConsumer()
        {
            _consumer?.Dispose();
        }
    }
}
