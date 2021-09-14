using Newtonsoft.Json;
using StackExchange.Redis;
using StackExchangeNetCoreTest.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading.Tasks;
using StackExchangeNetCoreTest.Models;

namespace StackExchangeNetCoreTest
{
    class Program
    {
        private const string Key = "key1";
        private static IDatabase _db;
        private static ConnectionMultiplexer redis;

        static void Main()
        {
            //Test1();
            Test2();
            //Test3();
            //Test4();
            //Test5();

            var a = default(UseInfo);


            Console.Read();
        }


        #region basic

        private static void Test1()
        {
            //https://stackexchange.github.io/StackExchange.Redis/Configuration
            //var conn = ConnectionMultiplexer.Connect("redis0:6380,redis1:6380,allowAdmin=true");

            //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("server1:6379,server2:6379");

            redis = ConnectionMultiplexer.Connect(Config.RedisConnection + ",allowAdmin=true");
           

            _db = redis.GetDatabase();
            //FlushAll();
            
            
            SetGet();
        }

        private static void Test2()
        {
            ConnectionByConfigurationOption.GetDb(ref _db);

            SetGet();
        }

        private static void Test3()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(Config.RedisConnection);
            _db = redis.GetDatabase();

            SetGetAsync();
        }

        private static void Test4()
        {
            redis = ConnectionMultiplexer.Connect(Config.RedisConnection + ",allowAdmin=true");
            _db = redis.GetDatabase();

            RedisResult ret1 = _db.Execute("keys", "a*");
            var ret2 =  (RedisResult[]) ret1;
        


            var ret = Keys();
            Console.WriteLine(JsonConvert.SerializeObject(ret));
        }

        private static void Test5()
        {
            redis = ConnectionMultiplexer.Connect(Config.RedisConnection + ",allowAdmin=true");
            _db = redis.GetDatabase();

            List<RedisKey> keys = new List<RedisKey>();
            keys.Add("key");
            keys.Add("key1");

            RedisValue[] ret = _db.StringGet(keys.ToArray());

        }

        private static void Test6()
        {
            redis = ConnectionMultiplexer.Connect(Config.RedisConnection + ",allowAdmin=true");
            _db = redis.GetDatabase();

            _db.StringSet("aaa", string.Empty);

        }

        #endregion
        
        #region common

        private static void SetGetAsync()
        {
            int i = 20;
            while (i >= 0)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                SetValueAsync();
                GetValueAsync().Wait();

                sw.Stop();
                Console.WriteLine($"-----------set and get cost:{sw.ElapsedMilliseconds}");

                i--;
            }
        }

        private static void SetGet()
        {
            int i = 20;
            while (i >= 0)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                SetValue();
                GetValue();

                sw.Stop();
                Console.WriteLine($"-----------set and get cost:{sw.ElapsedMilliseconds}");

                i--;
            }
        }

        private static void GetValue(string key = Key)
        {
            var value = _db.StringGet(key);

            byte[] ret1 = _db.StringGet(key);
            var ret2 = ret1.FromUtf8Bytes();

            var ret = (value.Box() as byte[]).FromUtf8Bytes();

            Console.Write(value);
            
        }


        private static void SetValue(string key = Key)
        {
            var value = DateTime.Now.Second;

            _db.StringSet(key, value);
            //_db.HashSet()
        }

        private static async Task GetValueAsync()
        {
            var asyncValue = _db.StringGetAsync(Key);
            var value = _db.Wait(asyncValue);

            var value1 = await _db.StringGetAsync(Key);


            Console.Write(value);
            Console.Write(value1);
        }

        private static void SetValueAsync()
        {
            var value = DateTime.Now.Second;

            _db.StringSetAsync(Key, value);
            
            //_db.HashIncrement() //实现自增
        }

        #endregion

        #region byte

        private static void SetGetBytes()
        {

        }

        private static void SetByte()
        {

        }

        #endregion

        public static void FlushAll()
        {
            var endpoints = redis.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = redis.GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }


        public static List<string> Keys()
        {
            List<string> keys = new List<string>();

            var endpoints = redis.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = redis.GetServer(endpoint);
                var tempKeys = server.Keys(pattern:"*");
                foreach (var redisKey in tempKeys)
                {
                    keys.Add(redisKey);
                }

                var ret = server.Keys(pattern:"*");
                foreach (var VARIABLE in ret)
                {
                    
                }
            }
            

            //RedisResult[] redisResults = (RedisResult[])_db.Execute("keys", "*");
            //foreach (var itemResult in redisResults)
            //{
            //    keys.Add(itemResult.ToString());
            //}

            return keys;
        }
    }
}
