using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace NancyConsole
{
    public static class ProtobufUtils
    {
        public static byte[] SerializeStruct<T>(this T value) where T : struct                      
        {                                                                                           
            using (var stream = new MemoryStream())                                                 
            {                                                                                       
                Serializer.Serialize(stream, value);                                                
                return stream.ToArray();                                                            
            }                                                                                       
        }                                                                                           

        public static byte[] SerializeClass<T>(this T value) where T : class, new()                 
        {                                                                                           
            if (value == null)                                                                      
            {                                                                                       
                return null;                                                                        
            }                                                                                       
            using (var stream = new MemoryStream())                                                 
            {                                                                                       
                Serializer.Serialize(stream, value);                                                
                return stream.ToArray();                                                            
            }                                                                                       
        }                                                                                           

        public static T DeserializeStruct<T>(this byte[] data) where T : struct                     
        {                                                                                           
            if (data == null || data.Length == 0)                                                   
            {                                                                                       
                throw new Exception("serialized byte[] is null or empty");                          
            }                                                                                       
            using (var stream = new MemoryStream(data))                                             
            {                                                                                       
                return Serializer.Deserialize<T>(stream);                                           
            }                                                                                       
        }                                                                                           

        public static T DeserializeClass<T>(this byte[] data) where T : class, new()                
        {                                                                                           
            if (data == null || data.Length == 0)                                                   
            {                                                                                       
                return null;                                                                        
            }                                                                                       
            using (var stream = new MemoryStream(data))                                             
            {                                                                                       
                return Serializer.Deserialize<T>(stream);                                           
            }                                                                                       
        }                                                                                           

        public static T DeserializeClassSubtype<T>(this byte[] data) where T : class, new()         
        {                                                                                           
            if (data == null || data.Length == 0)                                                   
            {                                                                                       
                return null;                                                                        
            }                                                                                       
            using (var stream = new MemoryStream(data))                                             
            {                                                                                       
                return Serializer.Merge(stream, new T());                                           
            }                                                                                       
        }
    }
}
