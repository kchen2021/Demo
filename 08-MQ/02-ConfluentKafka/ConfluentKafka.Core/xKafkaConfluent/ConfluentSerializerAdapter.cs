using ConfluentKafka.Core.xSerialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfluentKafka.Core.xKafkaConfluent
{
    public class ConfluentSerializerAdapter<T> :
        Confluent.Kafka.Serialization.ISerializer<T>,
        Confluent.Kafka.Serialization.IDeserializer<T>
    {
        public ConfluentSerializerAdapter(ISerializer<T> serializer)
        {
            _serializer = serializer;
        }
        private readonly ISerializer<T> _serializer;
        public void Dispose()
        { }
        public byte[] Serialize(string topic, T data) => _serializer.Serialize(data);
        public T Deserialize(string topic, byte[] data) => _serializer.Deserialize(data);
        IEnumerable<KeyValuePair<string, object>> Confluent.Kafka.Serialization.IDeserializer<T>.Configure(
            IEnumerable<KeyValuePair<string, object>> config,
            bool isKey
        ) => config;
        IEnumerable<KeyValuePair<string, object>> Confluent.Kafka.Serialization.ISerializer<T>.Configure(
            IEnumerable<KeyValuePair<string, object>> config,
            bool isKey
        ) => config;
    }
}
