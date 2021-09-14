using System;
using System.Collections.Generic;
using System.Text;

namespace ConfluentKafka.Core.xSerialization
{
    public delegate byte[] SerializerHandler<in TValue>(TValue value);
    public delegate TValue DeserializerHandler<out TValue>(byte[] bytes);
    public interface ISerializer<TValue>
    {
        byte[] Serialize(TValue value);
        TValue Deserialize(byte[] message);
    }
}
