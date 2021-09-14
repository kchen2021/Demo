using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace ServiceStackTest
{
    class Program
    {
        private static RedisClient redisClient;

        static string key = "aaaa";
        static void Main(string[] args)
        {
            RedisEndpoint redisEndpoint = new RedisEndpoint("47.104.111.126", 6379);
            redisClient = new RedisClient(redisEndpoint);
            var r = redisClient.Info;
            Get();
            Set();
            Get();

            redisClient.Keys("");
            

            Console.Read();
        }


        private  static void Set()
        {
            string value = key + DateTime.Now.Second;
            var success = redisClient.Set<string>(key, value);

            Console.WriteLine(success);
        }


        private static void Get()
        {
            var value = redisClient.Get<string>(key);

            Console.WriteLine(value);
        }
    }
}
