using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfluentKafka.Core.xSerialization;

namespace ConfluentKafka.Core.xKafkaConfluent
{
    public class ConfluentProducer<TKey, TValue>
    {
        private Confluent.Kafka.Producer<TKey, TValue> _producer;
        private string _topicName;

        public ConfluentProducer(string topicName,
            IReadOnlyDictionary<string, object> config,
            ISerializer<TKey> keySerializer,
            ISerializer<TValue> serializer)
        {
            _producer = new Confluent.Kafka.Producer<TKey, TValue>(
                config,
                new ConfluentSerializerAdapter<TKey>(keySerializer),
                new ConfluentSerializerAdapter<TValue>(serializer)
            );
            _topicName = topicName;
        }


        public Task SendAsync(IEnumerable<KeyValuePair<TKey, TValue>> messages) =>
            Task.WhenAll(messages.Select(i => OnSendAsync(i.Key, i.Value)));

        public Task SendAsync(TKey key, TValue value) =>
            OnSendAsync(key, value);

        private Task OnSendAsync(TKey key, TValue message) =>
            _producer.ProduceAsync(_topicName, key, message);
    }
}
