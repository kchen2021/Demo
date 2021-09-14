using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace StackExchangeTest
{
    public class RedisClient : IDisposable
    {
        private IConfigurationRoot _config;
        private ConcurrentDictionary<string, ConnectionMultiplexer> _connections;

        public RedisClient(IConfigurationRoot config)
        {
            _config = config;
            _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        }

        /// <summary>
        /// 获取ConnectionMultiplexer
        /// </summary>
        /// <param name="redisConfig">RedisConfig配置文件</param>
        /// <returns></returns>
        private ConnectionMultiplexer GetConnect(IConfigurationSection redisConfig)
        {
            var redisInstanceName = redisConfig["InstanceName"];
            var connStr = redisConfig["Connection"];
            return _connections.GetOrAdd(redisInstanceName, p => ConnectionMultiplexer.Connect(connStr));
        }

        /// <summary>
        /// 检查入参数
        /// </summary>
        /// <param name="configName">RedisConfig配置文件中的 Redis_Default/Redis_6 名称</param>
        /// <returns></returns>
        private IConfigurationSection CheckeConfig(string configName)
        {
            IConfigurationSection redisConfig = _config.GetSection("RedisConfig").GetSection(configName);
            if (redisConfig == null)
            {
                throw new ArgumentNullException($"{configName}找不到对应的RedisConfig配置！");
            }

            var redisInstanceName = redisConfig["InstanceName"];
            var connStr = redisConfig["Connection"];
            if (string.IsNullOrEmpty(redisInstanceName))
            {
                throw new ArgumentNullException($"{configName}找不到对应的InstanceName");
            }

            if (string.IsNullOrEmpty(connStr))
            {
                throw new ArgumentNullException($"{configName}找不到对应的Connection");
            }

            return redisConfig;
        }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="db">默认为0：优先代码的db配置，其次config中的配置</param>
        /// <returns></returns>
        public IDatabase GetDatabase(string configName = null, int? db = null)
        {
            int defaultDb = 0;
            IConfigurationSection redisConfig = CheckeConfig(configName);
            if (db.HasValue)
            {
                defaultDb = db.Value;
            }
            else
            {
                var strDefalutDatabase = redisConfig["DefaultDatabase"];
                if (!string.IsNullOrEmpty(strDefalutDatabase) &&
                    int.TryParse(strDefalutDatabase, out int intDefaultDatabase))
                {
                    defaultDb = intDefaultDatabase;
                }
            }

            return GetConnect(redisConfig).GetDatabase(defaultDb);
        }

        public void Dispose()
        {
            if (_connections != null && _connections.Count > 0)
            {
                foreach (var item in _connections.Values)
                {
                    item.Close();
                }
            }
        }
    }
}
