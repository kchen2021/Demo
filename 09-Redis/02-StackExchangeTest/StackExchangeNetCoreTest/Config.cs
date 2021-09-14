using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchangeNetCoreTest
{
    public class Config
    {
        public static string RedisConnection
        {
            get
            {
                string conncetion = "staging.cache.xignite.org:13983";
                //conncetion = "47.104.111.126:6379"; // aliyun  redisserver.
                //"server1:6379,server2:6379"  eg from document.
                 

                return conncetion;
            }
        }
    }
}
