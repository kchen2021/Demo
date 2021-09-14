using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ProtoBuf;

namespace StackExchangeTest.Models
{
    //COGGED
    [ProtoContract]                                                                                 //COGGED
        [Serializable]                                                                                  //COGGED
        public partial class AdjustmentRatio                                                            //COGGED
        {                                                                                               //COGGED
            [ProtoMember(1, IsRequired = false, Name = @"Id", DataFormat = ProtoBuf.DataFormat.TwosComplement)]//COGGED
            [System.ComponentModel.DefaultValue(0)]                                                     //COGGED
            public long Id;                                                                             //COGGED

            [ProtoMember(2, IsRequired = false, Name = @"InsertDate", DataFormat = ProtoBuf.DataFormat.TwosComplement)]//COGGED
            [System.ComponentModel.DefaultValue(0)]                                                     //COGGED
            private long InsertDateSerializable                                                         //COGGED
            {                                                                                           //COGGED
                get
                {                                                                                   //COGGED
                    return DateTimeToTicksSinceEpoch(InsertDate);                                       //COGGED
                }                                                                                       //COGGED
                set
                {                                                                                   //COGGED
                    InsertDate = TicksSinceEpochToDateTime(value);                                      //COGGED
                }                                                                                       //COGGED
            }                                                                                           //COGGED
            public DateTime InsertDate;                                                                 //COGGED

            [ProtoMember(3, IsRequired = false, Name = @"LastUpdateDate", DataFormat = ProtoBuf.DataFormat.TwosComplement)]//COGGED
            [System.ComponentModel.DefaultValue(0)]                                                     //COGGED
            private long LastUpdateDateSerializable                                                     //COGGED
            {                                                                                           //COGGED
                get
                {                                                                                   //COGGED
                    return DateTimeToTicksSinceEpoch(LastUpdateDate);                                   //COGGED
                }                                                                                       //COGGED
                set
                {                                                                                   //COGGED
                    LastUpdateDate = TicksSinceEpochToDateTime(value);                                  //COGGED
                }                                                                                       //COGGED
            }                                                                                           //COGGED
            public DateTime LastUpdateDate;                                                             //COGGED

            [ProtoMember(4, IsRequired = false, Name = @"SplitAndDividendId", DataFormat = ProtoBuf.DataFormat.TwosComplement)]//COGGED
            [System.ComponentModel.DefaultValue(0)]                                                     //COGGED
            public long SplitAndDividendId;                                                             //COGGED

            [ProtoMember(5, IsRequired = false, Name = @"IntegerId", DataFormat = ProtoBuf.DataFormat.TwosComplement)]//COGGED
            [System.ComponentModel.DefaultValue(0)]                                                     //COGGED
            public long IntegerId;                                                                      //COGGED

            [ProtoMember(6, IsRequired = false, Name = @"OldAmount", DataFormat = ProtoBuf.DataFormat.Default)]//COGGED
            [System.ComponentModel.DefaultValue(null)]                                                  //COGGED
            public decimal OldAmount;                                                                   //COGGED

            [ProtoMember(7, IsRequired = false, Name = @"NewAmount", DataFormat = ProtoBuf.DataFormat.Default)]//COGGED
            [System.ComponentModel.DefaultValue(null)]                                                  //COGGED
            public decimal NewAmount;                                                                   //COGGED

            [ProtoMember(8, IsRequired = false, Name = @"ExDate", DataFormat = ProtoBuf.DataFormat.TwosComplement)]//COGGED
            [System.ComponentModel.DefaultValue(0)]                                                     //COGGED
            private long ExDateSerializable                                                             //COGGED
            {                                                                                           //COGGED
                get
                {                                                                                   //COGGED
                    return DateTimeToTicksSinceEpoch(ExDate);                                           //COGGED
                }                                                                                       //COGGED
                set
                {                                                                                   //COGGED
                    ExDate = TicksSinceEpochToDateTime(value);                                          //COGGED
                }                                                                                       //COGGED
            }                                                                                           //COGGED
            public DateTime ExDate;                                                                     //COGGED

            [ProtoMember(9, IsRequired = false, Name = @"CumulativeSplitNumerator", DataFormat = ProtoBuf.DataFormat.Default)]//COGGED
            [System.ComponentModel.DefaultValue(null)]                                                  //COGGED
            public decimal CumulativeSplitNumerator;                                                    //COGGED

            [ProtoMember(10, IsRequired = false, Name = @"CumulativeSplitDenominator", DataFormat = ProtoBuf.DataFormat.Default)]//COGGED
            [System.ComponentModel.DefaultValue(null)]                                                  //COGGED
            public decimal CumulativeSplitDenominator;                                                  //COGGED

            [ProtoMember(11, IsRequired = false, Name = @"CumulativeCashDividendNumerator", DataFormat = ProtoBuf.DataFormat.Default)]//COGGED
            [System.ComponentModel.DefaultValue(null)]                                                  //COGGED
            public decimal CumulativeCashDividendNumerator;                                             //COGGED

            [ProtoMember(12, IsRequired = false, Name = @"CumulativeCashDividendDenominator", DataFormat = ProtoBuf.DataFormat.Default)]//COGGED
            [System.ComponentModel.DefaultValue(null)]                                                  //COGGED
            public decimal CumulativeCashDividendDenominator;                                           //COGGED

            [ProtoMember(13, IsRequired = false, Name = @"CumulativeStockDividendNumerator", DataFormat = ProtoBuf.DataFormat.Default)]//COGGED
            [System.ComponentModel.DefaultValue(null)]                                                  //COGGED
            public decimal CumulativeStockDividendNumerator;                                            //COGGED

            [ProtoMember(14, IsRequired = false, Name = @"CumulativeStockDividendDenominator", DataFormat = ProtoBuf.DataFormat.Default)]//COGGED
            [System.ComponentModel.DefaultValue(null)]                                                  //COGGED
            public decimal CumulativeStockDividendDenominator;                                          //COGGED

            [ProtoMember(15, IsRequired = false, Name = @"CumulativeOtherAsCashDividendNumerator", DataFormat = ProtoBuf.DataFormat.Default)]//COGGED
            [System.ComponentModel.DefaultValue(null)]                                                  //COGGED
            public decimal CumulativeOtherAsCashDividendNumerator;                                      //COGGED

            [ProtoMember(16, IsRequired = false, Name = @"CumulativeOtherAsCashDividendDenominator", DataFormat = ProtoBuf.DataFormat.Default)]//COGGED
            [System.ComponentModel.DefaultValue(null)]                                                  //COGGED
            public decimal CumulativeOtherAsCashDividendDenominator;                                    //COGGED

            [ProtoMember(17, IsRequired = false, Name = @"CumulativeExtraordinaryCashDividendNumerator", DataFormat = ProtoBuf.DataFormat.Default)]//COGGED
            [System.ComponentModel.DefaultValue(null)]                                                  //COGGED
            public decimal CumulativeExtraordinaryCashDividendNumerator;                                //COGGED

            [ProtoMember(18, IsRequired = false, Name = @"CumulativeExtraordinaryCashDividendDenominator", DataFormat = ProtoBuf.DataFormat.Default)]//COGGED
            [System.ComponentModel.DefaultValue(null)]                                                  //COGGED
            public decimal CumulativeExtraordinaryCashDividendDenominator;                              //COGGED

            [ProtoMember(19, IsRequired = false, Name = @"TotalCumulativeNumerator", DataFormat = ProtoBuf.DataFormat.Default)]//COGGED
            [System.ComponentModel.DefaultValue(null)]                                                  //COGGED
            public decimal TotalCumulativeNumerator;                                                    //COGGED

            [ProtoMember(20, IsRequired = false, Name = @"TotalCumulativeDenominator", DataFormat = ProtoBuf.DataFormat.Default)]//COGGED
            [System.ComponentModel.DefaultValue(null)]                                                  //COGGED
            public decimal TotalCumulativeDenominator;                                                  //COGGED

            public AdjustmentRatio(AdjustmentRatio sourceAdjustmentRatio)                               //COGGED
            {                                                                                           //COGGED
                Copy(sourceAdjustmentRatio);                                                            //COGGED
            }                                                                                           //COGGED

            public void Copy(AdjustmentRatio sourceAdjustmentRatio)                                     //COGGED
            {                                                                                           //COGGED
                this.Id = sourceAdjustmentRatio.Id;                                                     //COGGED
                this.InsertDate = sourceAdjustmentRatio.InsertDate;                                     //COGGED
                this.LastUpdateDate = sourceAdjustmentRatio.LastUpdateDate;                             //COGGED
                this.SplitAndDividendId = sourceAdjustmentRatio.SplitAndDividendId;                     //COGGED
                this.IntegerId = sourceAdjustmentRatio.IntegerId;                                       //COGGED
                this.OldAmount = sourceAdjustmentRatio.OldAmount;                                       //COGGED
                this.NewAmount = sourceAdjustmentRatio.NewAmount;                                       //COGGED
                this.ExDate = sourceAdjustmentRatio.ExDate;                                             //COGGED
                this.CumulativeSplitNumerator = sourceAdjustmentRatio.CumulativeSplitNumerator;         //COGGED
                this.CumulativeSplitDenominator = sourceAdjustmentRatio.CumulativeSplitDenominator;     //COGGED
                this.CumulativeCashDividendNumerator = sourceAdjustmentRatio.CumulativeCashDividendNumerator;//COGGED
                this.CumulativeCashDividendDenominator = sourceAdjustmentRatio.CumulativeCashDividendDenominator;//COGGED
                this.CumulativeStockDividendNumerator = sourceAdjustmentRatio.CumulativeStockDividendNumerator;//COGGED
                this.CumulativeStockDividendDenominator = sourceAdjustmentRatio.CumulativeStockDividendDenominator;//COGGED
                this.CumulativeOtherAsCashDividendNumerator = sourceAdjustmentRatio.CumulativeOtherAsCashDividendNumerator;//COGGED
                this.CumulativeOtherAsCashDividendDenominator = sourceAdjustmentRatio.CumulativeOtherAsCashDividendDenominator;//COGGED
                this.CumulativeExtraordinaryCashDividendNumerator = sourceAdjustmentRatio.CumulativeExtraordinaryCashDividendNumerator;//COGGED
                this.CumulativeExtraordinaryCashDividendDenominator = sourceAdjustmentRatio.CumulativeExtraordinaryCashDividendDenominator;//COGGED
                this.TotalCumulativeNumerator = sourceAdjustmentRatio.TotalCumulativeNumerator;         //COGGED
                this.TotalCumulativeDenominator = sourceAdjustmentRatio.TotalCumulativeDenominator;     //COGGED
            }                                                                                           //COGGED

            public AdjustmentRatio()                                                                    //COGGED
            {                                                                                           //COGGED
            }                                                                                           //COGGED

            public static class Columns                                                                 //COGGED
            {                                                                                           //COGGED
                public const string Id = @"Id";                                                         //COGGED
                public const string InsertDate = @"InsertDate";                                         //COGGED
                public const string LastUpdateDate = @"LastUpdateDate";                                 //COGGED
                public const string SplitAndDividendId = @"SplitAndDividendId";                         //COGGED
                public const string IntegerId = @"IntegerId";                                           //COGGED
                public const string OldAmount = @"OldAmount";                                           //COGGED
                public const string NewAmount = @"NewAmount";                                           //COGGED
                public const string ExDate = @"ExDate";                                                 //COGGED
                public const string CumulativeSplitNumerator = @"CumulativeSplitNumerator";             //COGGED
                public const string CumulativeSplitDenominator = @"CumulativeSplitDenominator";         //COGGED
                public const string CumulativeCashDividendNumerator = @"CumulativeCashDividendNumerator";//COGGED
                public const string CumulativeCashDividendDenominator = @"CumulativeCashDividendDenominator";//COGGED
                public const string CumulativeStockDividendNumerator = @"CumulativeStockDividendNumerator";//COGGED
                public const string CumulativeStockDividendDenominator = @"CumulativeStockDividendDenominator";//COGGED
                public const string CumulativeOtherAsCashDividendNumerator = @"CumulativeOtherAsCashDividendNumerator";//COGGED
                public const string CumulativeOtherAsCashDividendDenominator = @"CumulativeOtherAsCashDividendDenominator";//COGGED
                public const string CumulativeExtraordinaryCashDividendNumerator = @"CumulativeExtraordinaryCashDividendNumerator";//COGGED
                public const string CumulativeExtraordinaryCashDividendDenominator = @"CumulativeExtraordinaryCashDividendDenominator";//COGGED
                public const string TotalCumulativeNumerator = @"TotalCumulativeNumerator";             //COGGED
                public const string TotalCumulativeDenominator = @"TotalCumulativeDenominator";         //COGGED
            }                                                                                           //COGGED

            private static DataTable CreateDynamicDatabaseDataTable()                                   //COGGED
            {                                                                                           //COGGED
                DataTable dataTable = new DataTable();                                                  //COGGED
                dataTable.Columns.Add(Columns.Id, typeof(long));                                        //COGGED
                dataTable.Columns.Add(Columns.InsertDate, typeof(DateTime));                            //COGGED
                dataTable.Columns.Add(Columns.LastUpdateDate, typeof(DateTime));                        //COGGED
                dataTable.Columns.Add(Columns.SplitAndDividendId, typeof(long));                        //COGGED
                dataTable.Columns.Add(Columns.IntegerId, typeof(long));                                 //COGGED
                dataTable.Columns.Add(Columns.OldAmount, typeof(decimal));                              //COGGED
                dataTable.Columns.Add(Columns.NewAmount, typeof(decimal));                              //COGGED
                dataTable.Columns.Add(Columns.ExDate, typeof(DateTime));                                //COGGED
                dataTable.Columns.Add(Columns.CumulativeSplitNumerator, typeof(decimal));               //COGGED
                dataTable.Columns.Add(Columns.CumulativeSplitDenominator, typeof(decimal));             //COGGED
                dataTable.Columns.Add(Columns.CumulativeCashDividendNumerator, typeof(decimal));        //COGGED
                dataTable.Columns.Add(Columns.CumulativeCashDividendDenominator, typeof(decimal));      //COGGED
                dataTable.Columns.Add(Columns.CumulativeStockDividendNumerator, typeof(decimal));       //COGGED
                dataTable.Columns.Add(Columns.CumulativeStockDividendDenominator, typeof(decimal));     //COGGED
                dataTable.Columns.Add(Columns.CumulativeOtherAsCashDividendNumerator, typeof(decimal)); //COGGED
                dataTable.Columns.Add(Columns.CumulativeOtherAsCashDividendDenominator, typeof(decimal));//COGGED
                dataTable.Columns.Add(Columns.CumulativeExtraordinaryCashDividendNumerator, typeof(decimal));//COGGED
                dataTable.Columns.Add(Columns.CumulativeExtraordinaryCashDividendDenominator, typeof(decimal));//COGGED
                dataTable.Columns.Add(Columns.TotalCumulativeNumerator, typeof(decimal));               //COGGED
                dataTable.Columns.Add(Columns.TotalCumulativeDenominator, typeof(decimal));             //COGGED
                return dataTable;                                                                       //COGGED
            }                                                                                           //COGGED

            public DataRow ConvertToDataRow(AdjustmentRatio AdjustmentRatio, DataTable DataTable)       //COGGED
            {                                                                                           //COGGED
                DataRow dataRow = DataTable.NewRow();                                                   //COGGED
                dataRow[Columns.Id] = (long)AdjustmentRatio.Id;                                     //COGGED
                dataRow[Columns.InsertDate] = (DateTime)AdjustmentRatio.InsertDate;                 //COGGED
                dataRow[Columns.LastUpdateDate] = (DateTime)AdjustmentRatio.LastUpdateDate;         //COGGED
                dataRow[Columns.SplitAndDividendId] = (long)AdjustmentRatio.SplitAndDividendId;     //COGGED
                dataRow[Columns.IntegerId] = (long)AdjustmentRatio.IntegerId;                           //COGGED
                dataRow[Columns.OldAmount] = (decimal)AdjustmentRatio.OldAmount;                        //COGGED
                dataRow[Columns.NewAmount] = (decimal)AdjustmentRatio.NewAmount;                        //COGGED
                dataRow[Columns.ExDate] = (DateTime)AdjustmentRatio.ExDate;                         //COGGED
                dataRow[Columns.CumulativeSplitNumerator] = (decimal)AdjustmentRatio.CumulativeSplitNumerator;//COGGED
                dataRow[Columns.CumulativeSplitDenominator] = (decimal)AdjustmentRatio.CumulativeSplitDenominator;//COGGED
                dataRow[Columns.CumulativeCashDividendNumerator] = (decimal)AdjustmentRatio.CumulativeCashDividendNumerator;//COGGED
                dataRow[Columns.CumulativeCashDividendDenominator] = (decimal)AdjustmentRatio.CumulativeCashDividendDenominator;//COGGED
                dataRow[Columns.CumulativeStockDividendNumerator] = (decimal)AdjustmentRatio.CumulativeStockDividendNumerator;//COGGED
                dataRow[Columns.CumulativeStockDividendDenominator] = (decimal)AdjustmentRatio.CumulativeStockDividendDenominator;//COGGED
                dataRow[Columns.CumulativeOtherAsCashDividendNumerator] = (decimal)AdjustmentRatio.CumulativeOtherAsCashDividendNumerator;//COGGED
                dataRow[Columns.CumulativeOtherAsCashDividendDenominator] = (decimal)AdjustmentRatio.CumulativeOtherAsCashDividendDenominator;//COGGED
                dataRow[Columns.CumulativeExtraordinaryCashDividendNumerator] = (decimal)AdjustmentRatio.CumulativeExtraordinaryCashDividendNumerator;//COGGED
                dataRow[Columns.CumulativeExtraordinaryCashDividendDenominator] = (decimal)AdjustmentRatio.CumulativeExtraordinaryCashDividendDenominator;//COGGED
                dataRow[Columns.TotalCumulativeNumerator] = (decimal)AdjustmentRatio.TotalCumulativeNumerator;//COGGED
                dataRow[Columns.TotalCumulativeDenominator] = (decimal)AdjustmentRatio.TotalCumulativeDenominator;//COGGED
                return dataRow;                                                                         //COGGED
            }                                                                                           //COGGED

            public DataTable ConvertListToDataTable<T>(IEnumerable<T> AdjustmentRatioCollection) where T : AdjustmentRatio, new()//COGGED
            {                                                                                           //COGGED
                DataTable dataTable = AdjustmentRatio.CreateDynamicDatabaseDataTable();                 //COGGED
                foreach (AdjustmentRatio item in AdjustmentRatioCollection)                             //COGGED
                {                                                                                       //COGGED
                    dataTable.Rows.Add(ConvertToDataRow(item, dataTable));                              //COGGED
                }                                                                                       //COGGED
                return dataTable;                                                                       //COGGED
            }                                                                                           //COGGED

            public T ConvertToObject<T>(DataRow AdjustmentRatioDataRow) where T : AdjustmentRatio, new()//COGGED
            {                                                                                           //COGGED
                T ret = new T();                                                                        //COGGED
                ret.Id = AdjustmentRatioDataRow.IsNull("Id") ? default(long) : long.Parse(AdjustmentRatioDataRow["Id"].ToString());//COGGED
                ret.InsertDate = AdjustmentRatioDataRow.IsNull("InsertDate") ? default(DateTime) : (DateTime)AdjustmentRatioDataRow["InsertDate"];//COGGED
                ret.LastUpdateDate = AdjustmentRatioDataRow.IsNull("LastUpdateDate") ? default(DateTime) : (DateTime)AdjustmentRatioDataRow["LastUpdateDate"];//COGGED
                ret.SplitAndDividendId = AdjustmentRatioDataRow.IsNull("SplitAndDividendId") ? default(long) : long.Parse(AdjustmentRatioDataRow["SplitAndDividendId"].ToString());//COGGED
                ret.IntegerId = AdjustmentRatioDataRow.IsNull("IntegerId") ? default(long) : long.Parse(AdjustmentRatioDataRow["IntegerId"].ToString());//COGGED
                ret.OldAmount = AdjustmentRatioDataRow.IsNull("OldAmount") ? default(decimal) : Convert.ToDecimal(AdjustmentRatioDataRow["OldAmount"]);//COGGED
                ret.NewAmount = AdjustmentRatioDataRow.IsNull("NewAmount") ? default(decimal) : Convert.ToDecimal(AdjustmentRatioDataRow["NewAmount"]);//COGGED
                ret.ExDate = AdjustmentRatioDataRow.IsNull("ExDate") ? default(DateTime) : (DateTime)AdjustmentRatioDataRow["ExDate"];//COGGED
                ret.CumulativeSplitNumerator = AdjustmentRatioDataRow.IsNull("CumulativeSplitNumerator") ? default(decimal) : Convert.ToDecimal(AdjustmentRatioDataRow["CumulativeSplitNumerator"]);//COGGED
                ret.CumulativeSplitDenominator = AdjustmentRatioDataRow.IsNull("CumulativeSplitDenominator") ? default(decimal) : Convert.ToDecimal(AdjustmentRatioDataRow["CumulativeSplitDenominator"]);//COGGED
                ret.CumulativeCashDividendNumerator = AdjustmentRatioDataRow.IsNull("CumulativeCashDividendNumerator") ? default(decimal) : Convert.ToDecimal(AdjustmentRatioDataRow["CumulativeCashDividendNumerator"]);//COGGED
                ret.CumulativeCashDividendDenominator = AdjustmentRatioDataRow.IsNull("CumulativeCashDividendDenominator") ? default(decimal) : Convert.ToDecimal(AdjustmentRatioDataRow["CumulativeCashDividendDenominator"]);//COGGED
                ret.CumulativeStockDividendNumerator = AdjustmentRatioDataRow.IsNull("CumulativeStockDividendNumerator") ? default(decimal) : Convert.ToDecimal(AdjustmentRatioDataRow["CumulativeStockDividendNumerator"]);//COGGED
                ret.CumulativeStockDividendDenominator = AdjustmentRatioDataRow.IsNull("CumulativeStockDividendDenominator") ? default(decimal) : Convert.ToDecimal(AdjustmentRatioDataRow["CumulativeStockDividendDenominator"]);//COGGED
                ret.CumulativeOtherAsCashDividendNumerator = AdjustmentRatioDataRow.IsNull("CumulativeOtherAsCashDividendNumerator") ? default(decimal) : Convert.ToDecimal(AdjustmentRatioDataRow["CumulativeOtherAsCashDividendNumerator"]);//COGGED
                ret.CumulativeOtherAsCashDividendDenominator = AdjustmentRatioDataRow.IsNull("CumulativeOtherAsCashDividendDenominator") ? default(decimal) : Convert.ToDecimal(AdjustmentRatioDataRow["CumulativeOtherAsCashDividendDenominator"]);//COGGED
                ret.CumulativeExtraordinaryCashDividendNumerator = AdjustmentRatioDataRow.IsNull("CumulativeExtraordinaryCashDividendNumerator") ? default(decimal) : Convert.ToDecimal(AdjustmentRatioDataRow["CumulativeExtraordinaryCashDividendNumerator"]);//COGGED
                ret.CumulativeExtraordinaryCashDividendDenominator = AdjustmentRatioDataRow.IsNull("CumulativeExtraordinaryCashDividendDenominator") ? default(decimal) : Convert.ToDecimal(AdjustmentRatioDataRow["CumulativeExtraordinaryCashDividendDenominator"]);//COGGED
                ret.TotalCumulativeNumerator = AdjustmentRatioDataRow.IsNull("TotalCumulativeNumerator") ? default(decimal) : Convert.ToDecimal(AdjustmentRatioDataRow["TotalCumulativeNumerator"]);//COGGED
                ret.TotalCumulativeDenominator = AdjustmentRatioDataRow.IsNull("TotalCumulativeDenominator") ? default(decimal) : Convert.ToDecimal(AdjustmentRatioDataRow["TotalCumulativeDenominator"]);//COGGED
                return ret;                                                                             //COGGED
            }                                                                                           //COGGED

            public IEnumerable<T> ConvertToCollection<T>(DataTable AdjustmentRatioDataTable) where T : AdjustmentRatio, new()//COGGED
            {                                                                                           //COGGED
                foreach (DataRow dr in AdjustmentRatioDataTable.Rows)                                   //COGGED
                {                                                                                       //COGGED
                    yield return this.ConvertToObject<T>(dr);                                           //COGGED
                }                                                                                       //COGGED
            }                                                                                           //COGGED

            public static byte[] CachedObjectSerializer(object value)                                   //COGGED
            {                                                                                           //COGGED
                if (!(value is AdjustmentRatio))                                                        //COGGED
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
                if (!(values is IList<AdjustmentRatio>))                                                //COGGED
                {                                                                                       //COGGED
                    return null;                                                                        //COGGED
                }                                                                                       //COGGED
                using (var stream = new MemoryStream())                                                 //COGGED
                {                                                                                       //COGGED
                    Serializer.Serialize(stream, values);                                               //COGGED
                    return stream.ToArray();                                                            //COGGED
                }                                                                                       //COGGED
            }                                                                                           //COGGED

            public static AdjustmentRatio CachedObjectDeserializer(byte[] data)                         //COGGED
            {                                                                                           //COGGED
                if (data == null || data.Length == 0)                                                   //COGGED
                {                                                                                       //COGGED
                    return default(AdjustmentRatio);                                                    //COGGED
                }                                                                                       //COGGED
                using (var stream = new MemoryStream(data))                                             //COGGED
                {                                                                                       //COGGED
                    return Serializer.Deserialize<AdjustmentRatio>(stream);                             //COGGED
                }                                                                                       //COGGED
            }                                                                                           //COGGED

            public static AdjustmentRatio CachedObjectDeserializerSubtype(byte[] data)                  //COGGED
            {                                                                                           //COGGED
                if (data == null || data.Length == 0)                                                   //COGGED
                {                                                                                       //COGGED
                    return default(AdjustmentRatio);                                                    //COGGED
                }                                                                                       //COGGED
                using (var stream = new MemoryStream(data))                                             //COGGED
                {                                                                                       //COGGED
                    return Serializer.Merge(stream, new AdjustmentRatio());                             //COGGED
                }                                                                                       //COGGED
            }                                                                                           //COGGED

            public static IList<AdjustmentRatio> CachedObjectsDeserializer(byte[] data)                 //COGGED
            {                                                                                           //COGGED
                if (data == null || data.Length == 0)                                                   //COGGED
                {                                                                                       //COGGED
                    return default(IList<AdjustmentRatio>);                                             //COGGED
                }                                                                                       //COGGED
                using (var stream = new MemoryStream(data))                                             //COGGED
                {                                                                                       //COGGED
                    return Serializer.Deserialize<IList<AdjustmentRatio>>(stream);                      //COGGED
                }                                                                                       //COGGED
            }                                                                                           //COGGED


            static readonly long originTicks = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).Ticks;//COGGED

            private long DateTimeToTicksSinceEpoch(DateTime value)                                      //COGGED
            {                                                                                           //COGGED
                return value.Ticks - originTicks;                                                       //COGGED
            }                                                                                           //COGGED

            private DateTime TicksSinceEpochToDateTime(long ticks)                                      //COGGED
            {                                                                                           //COGGED
                                                                                                        // workaround for previous version of generated entity-classes:							//COGGED
                if (ticks == long.MinValue)                                                             //COGGED
                {                                                                                       //COGGED
                    return DateTime.MinValue;                                                           //COGGED
                }                                                                                       //COGGED
                else if (ticks == long.MaxValue)                                                        //COGGED
                {                                                                                       //COGGED
                    return DateTime.MaxValue;                                                           //COGGED
                }                                                                                       //COGGED
                else                                                                                    //COGGED
                {                                                                                       //COGGED
                    return new DateTime(originTicks + ticks);                                           //COGGED
                }                                                                                       //COGGED
            }                                                                                           //COGGED

        }

}
