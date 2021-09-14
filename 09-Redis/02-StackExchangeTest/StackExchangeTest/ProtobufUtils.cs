using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackExchangeTest
{                                                                                                   //COGGED
    public static class ProtobufUtils                                                               //COGGED
    {                                                                                               //COGGED

        public static byte[] SerializeStruct<T>(this T value) where T : struct                      //COGGED
        {                                                                                           //COGGED
            using (var stream = new MemoryStream())                                                 //COGGED
            {                                                                                       //COGGED
                Serializer.Serialize(stream, value);                                                //COGGED
                return stream.ToArray();                                                            //COGGED
            }                                                                                       //COGGED
        }                                                                                           //COGGED

        public static byte[] SerializeClass<T>(this T value) where T : class, new()                 //COGGED
        {                                                                                           //COGGED
            if (value == null)                                                                      //COGGED
            {                                                                                       //COGGED
                return null;                                                                        //COGGED
            }                                                                                       //COGGED
            using (var stream = new MemoryStream())                                                 //COGGED
            {                                                                                       //COGGED
                Serializer.Serialize(stream, value);                                                //COGGED
                return stream.ToArray();                                                            //COGGED
            }                                                                                       //COGGED
        }                                                                                           //COGGED

        public static T DeserializeStruct<T>(this byte[] data) where T : struct                     //COGGED
        {                                                                                           //COGGED
            if (data == null || data.Length == 0)                                                   //COGGED
            {                                                                                       //COGGED
                throw new Exception("serialized byte[] is null or empty");                          //COGGED
            }                                                                                       //COGGED
            using (var stream = new MemoryStream(data))                                             //COGGED
            {                                                                                       //COGGED
                return Serializer.Deserialize<T>(stream);                                           //COGGED
            }                                                                                       //COGGED
        }                                                                                           //COGGED

        public static T DeserializeClass<T>(this byte[] data) where T : class, new()                //COGGED
        {                                                                                           //COGGED
            if (data == null || data.Length == 0)                                                   //COGGED
            {                                                                                       //COGGED
                return null;                                                                        //COGGED
            }                                                                                       //COGGED
            using (var stream = new MemoryStream(data))                                             //COGGED
            {                                                                                       //COGGED
                return Serializer.Deserialize<T>(stream);                                           //COGGED
            }                                                                                       //COGGED
        }                                                                                           //COGGED

        public static T DeserializeClassSubtype<T>(this byte[] data) where T : class, new()         //COGGED
        {                                                                                           //COGGED
            if (data == null || data.Length == 0)                                                   //COGGED
            {                                                                                       //COGGED
                return null;                                                                        //COGGED
            }                                                                                       //COGGED
            using (var stream = new MemoryStream(data))                                             //COGGED
            {                                                                                       //COGGED
                return Serializer.Merge(stream, new T());                                           //COGGED
            }                                                                                       //COGGED
        }                                                                                           //COGGED

    }																								//COGGED

}
