using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackExchangeTest.Models
{                                                                                                   //COGGED
    [ProtoContract]                                                                                 //COGGED
    [Serializable]                                                                                  //COGGED
    public partial class AdjustmentRatioBundle                                                      //COGGED
    {                                                                                               //COGGED
        [ProtoMember(1, IsRequired = false, Name = @"CacheKey", DataFormat = ProtoBuf.DataFormat.TwosComplement)]//COGGED
        [System.ComponentModel.DefaultValue(0)]                                                     //COGGED
        public long CacheKey;                                                                       //COGGED

        [ProtoMember(2, IsRequired = false, Name = @"AdjustmentRatios", DataFormat = ProtoBuf.DataFormat.Default)]//COGGED
        [System.ComponentModel.DefaultValue(null)]                                                  //COGGED
        public List<AdjustmentRatio> AdjustmentRatios;                                              //COGGED

        public AdjustmentRatioBundle()                                                              //COGGED
        {                                                                                           //COGGED
        }                                                                                           //COGGED

        public static byte[] CachedObjectSerializer(object value)                                   //COGGED
        {                                                                                           //COGGED
            if (!(value is AdjustmentRatioBundle))                                                  //COGGED
            {                                                                                       //COGGED
                return null;                                                                        //COGGED
            }                                                                                       //COGGED
            using (var stream = new MemoryStream())                                                 //COGGED
            {                                                                                       //COGGED
                Serializer.Serialize(stream, value);                                                //COGGED
                return stream.ToArray();                                                            //COGGED
            }                                                                                       //COGGED
        }                                                                                           //COGGED

        public static byte[] CachedObjectsSerializer(object values)                                 //COGGED
        {                                                                                           //COGGED
            if (!(values is IList<AdjustmentRatioBundle>))                                          //COGGED
            {                                                                                       //COGGED
                return null;                                                                        //COGGED
            }                                                                                       //COGGED
            using (var stream = new MemoryStream())                                                 //COGGED
            {                                                                                       //COGGED
                Serializer.Serialize(stream, values);                                               //COGGED
                return stream.ToArray();                                                            //COGGED
            }                                                                                       //COGGED
        }                                                                                           //COGGED

        public static AdjustmentRatioBundle CachedObjectDeserializer(byte[] data)                   //COGGED
        {                                                                                           //COGGED
            if (data == null || data.Length == 0)                                                   //COGGED
            {                                                                                       //COGGED
                return default(AdjustmentRatioBundle);                                              //COGGED
            }                                                                                       //COGGED
            using (var stream = new MemoryStream(data))                                             //COGGED
            {                                                                                       //COGGED
                return Serializer.Deserialize<AdjustmentRatioBundle>(stream);                       //COGGED
            }                                                                                       //COGGED
        }                                                                                           //COGGED

        public static AdjustmentRatioBundle CachedObjectDeserializerSubtype(byte[] data)            //COGGED
        {                                                                                           //COGGED
            if (data == null || data.Length == 0)                                                   //COGGED
            {                                                                                       //COGGED
                return default(AdjustmentRatioBundle);                                              //COGGED
            }                                                                                       //COGGED
            using (var stream = new MemoryStream(data))                                             //COGGED
            {                                                                                       //COGGED
                return Serializer.Merge(stream, new AdjustmentRatioBundle());                       //COGGED
            }                                                                                       //COGGED
        }                                                                                           //COGGED

        public static IList<AdjustmentRatioBundle> CachedObjectsDeserializer(byte[] data)           //COGGED
        {                                                                                           //COGGED
            if (data == null || data.Length == 0)                                                   //COGGED
            {                                                                                       //COGGED
                return default(IList<AdjustmentRatioBundle>);                                       //COGGED
            }                                                                                       //COGGED
            using (var stream = new MemoryStream(data))                                             //COGGED
            {                                                                                       //COGGED
                return Serializer.Deserialize<IList<AdjustmentRatioBundle>>(stream);                //COGGED
            }                                                                                       //COGGED
        }                                                                                           //COGGED

    }																								//COGGED

}
