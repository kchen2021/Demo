using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace StackExchangeTest
{
    public class RedisClientSingleton
    {
        private static RedisClient _redisClinet;
        private RedisClientSingleton() { }

        private static object _lockObj = new object();
        public static RedisClient GetInstance(IConfigurationRoot config)
        {
            if (_redisClinet == null)
            {
                lock (_lockObj)
                {
                    if (_redisClinet == null)
                    {
                        _redisClinet = new RedisClient(config);
                    }
                }
            }
            return _redisClinet;
        }
    }
}