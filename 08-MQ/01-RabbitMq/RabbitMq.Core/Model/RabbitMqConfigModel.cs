using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Core.Model
{
    public class RabbitMqConfigModel
    {
        #region host
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 服务器端口，默认是 5672
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 虚拟主机名称
        /// </summary>
        public string VirtualHost { get; set; }
        #endregion

        #region Queue
        [Required]
        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 是否持久化该队列
        /// </summary>
        public bool DurableQueue { get; set; }
        #endregion

        #region exchange
        /// <summary>
        /// 路由名称
        /// </summary>
        [Required]
        public string ExchangeName { get; set; }

        /// <summary>
        /// 路由的类型枚举
        /// </summary>
        public ExchangeTypeEnum ExchangeType { get; set; }

        /// <summary>
        /// 路由的关键字
        /// </summary>
        [Required]
        public string RoutingKey { get; set; }

        #endregion

        #region message
        /// <summary>
        /// 是否持久化队列中的消息
        /// </summary>
        public bool DurableMessage { get; set; }
        #endregion
    }
}
