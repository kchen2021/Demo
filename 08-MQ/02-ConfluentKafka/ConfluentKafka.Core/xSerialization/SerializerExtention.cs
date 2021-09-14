using System.Collections.Generic;

namespace ConfluentKafka.Core.xSerialization
{
    public class SerializerExtention
    {
        public delegate byte[] SerializerHandler<in TValue>(TValue value);
        public delegate TValue DeserializerHandler<out TValue>(byte[] bytes);


        #region Serializer
        public class TSerializer<T> : Confluent.Kafka.Serialization.ISerializer<T>
        {
            private readonly SerializerHandler<T> _cacheSerializer;

            public TSerializer(SerializerHandler<T> cacheSerializer)
            {
                _cacheSerializer = cacheSerializer;
            }

            public IEnumerable<KeyValuePair<string, object>> Configure(IEnumerable<KeyValuePair<string, object>> config, bool isKey)
            {
                return config;
            }

            public void Dispose()
            {
            }

            public byte[] Serialize(string topic, T data)
            {
                return _cacheSerializer(data);
            }
        }


        public class TDerializer<T> : Confluent.Kafka.Serialization.IDeserializer<T>
        {
            private readonly DeserializerHandler<T> _cacheDeserializer;
            public TDerializer(DeserializerHandler<T> cacheDeserializer)
            {
                _cacheDeserializer = cacheDeserializer;
            }
            public IEnumerable<KeyValuePair<string, object>> Configure(IEnumerable<KeyValuePair<string, object>> config, bool isKey)
            {
                return config;
            }

            public T Deserialize(string topic, byte[] data)
            {
                return _cacheDeserializer(data);
            }

            public void Dispose()
            {
            }
        }

        #endregion
    }
}
