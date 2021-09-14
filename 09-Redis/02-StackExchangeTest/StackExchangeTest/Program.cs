using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using StackExchangeTest.Models;

namespace StackExchangeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var isOfficial = false;
            IConfigurationBuilder builder;
            builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            if (isOfficial)
            {
                builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings_office.json");
            }
            IConfigurationRoot configuration = builder.Build();

            var redisClient = RedisClientSingleton.GetInstance(configuration);

            IDatabase redisDatabase = redisClient.GetDatabase("Redis_Default");

            //List<RatioInteger> integerIdList = GetDbIntegerId();

            //foreach (var item in integerIdList)
            //{
            //    string key = "96:" + item.IntegerId;
            //    RedisValue value = redisDatabase.StringGet(key);
            //    int sign = 0;
            //    while (sign == 0)
            //    {
            //        if (value.HasValue)
            //        {
            //            Console.WriteLine($"key is exist：{key}");
            //            sign = 1;
            //        }
            //        else
            //        {
            //            Thread.Sleep(1000);
            //            Console.WriteLine($"key is not exist：{key}");
            //        }
            //    }
            //}

            string key = "";
            while (true)
            {
                if (key.Equals("-0"))
                {
                    break;
                }
                Console.Write("read key:");
                key = "110:" + Console.ReadLine();
                Get(redisDatabase, key);
            }

            Console.Read();
        }

        static void Set(IDatabase redisDatabase, string key = "TestStrKey", string value = "TestStrValue")
        {
            var retSet = redisDatabase.StringSet(key, value);
            Console.WriteLine($"set:{retSet}");
        }

        static void Get(IDatabase redisDatabase, string key = "TestStrKey")
        {
            string get = redisDatabase.StringGet(key);
            Console.Write($"get:{get}");
            RedisValue value = redisDatabase.StringGet(key);
            if (value.HasValue)
            {
                byte[] b1 = (byte[]) value.Box();

                List<AdjustmentRatio> ret = ProtobufUtils.DeserializeClass<List<AdjustmentRatio>>(b1);
                if (ret.Count > 0)
                {
                    Console.Write("-----DeserializeClass success！");
                }
            }

            Console.WriteLine();
        }

        static void Del(IDatabase redisDatabase, string key = "TestStrKey")
        {
            var retDel = redisDatabase.KeyDelete(key);
            Console.WriteLine($"retDel:{retDel}");
        }





        #region GetDb IntegerId

        //private static List<RatioInteger> GetDbIntegerId()
        //{
        //    ProcedureService service = new ProcedureService();

        //    return service.Get<RatioInteger>("xi_GetAdjustmentRatioIntegerIds");
        //}

        //public class RatioInteger
        //{
        //    public long IntegerId { get; set; }
        //}

        #endregion

    }
}
