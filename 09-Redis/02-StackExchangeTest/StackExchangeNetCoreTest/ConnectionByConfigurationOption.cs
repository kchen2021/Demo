using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using StackExchange.Redis;

namespace StackExchangeNetCoreTest
{
    public class ConnectionByConfigurationOption
    {
        public static void GetDb(ref IDatabase db)
        {
            ////https://stackexchange.github.io/StackExchange.Redis/Configuration
            /// + ",staging.cache.xignite.org:13983"
            var options = ConfigurationOptions.Parse(Config.RedisConnection);

            options.AllowAdmin = true;
            //options.ClientName = "XIGNITE_KECHEN";
            //options.ConnectRetry = 3;//The number of times to repeat connect attempts during initial Connect
            //options.ConnectTimeout = 5000; //Timeout (ms) for connect operations
            //options.DefaultDatabase = -1; //Default database index, from 0 to databases - 1
            


            var conn = ConnectionMultiplexer.Connect(options);
            //Microsoft Azure Redis example with password
            //var conn = ConnectionMultiplexer.Connect("contoso5.redis.cache.windows.net,ssl=true,password=...");


            db = conn.GetDatabase();
            
            //((System.Net.IPEndPoint)options.EndPoints[0]).Port


        }
    }
}
