using System;
using System.Collections.Generic;
using System.Text;

namespace ConfluentKafka.Core.xSerialization
{
    public class Serializer<TValue> : ISerializer<TValue>
    {
        public Serializer(
            SerializerHandler<TValue> serializer,
            DeserializerHandler<TValue> deserializer
        )
        {
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            if (deserializer == null) throw new ArgumentNullException(nameof(deserializer));
            _cacheSerializer = serializer;
            _cacheDeserializer = deserializer;
        }

        private readonly SerializerHandler<TValue> _cacheSerializer;
        private readonly DeserializerHandler<TValue> _cacheDeserializer;

        public byte[] Serialize(TValue value) =>
            _cacheSerializer(value);

        public TValue Deserialize(byte[] message)
        {
            if (null == message)
            {
                return default(TValue);
            }
            return _cacheDeserializer(message);
        }

    
}
}
