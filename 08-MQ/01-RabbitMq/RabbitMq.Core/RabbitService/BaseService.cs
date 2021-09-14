using RabbitMq.Core.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RabbitMq.Core.RabbitService
{
    /// <summary>
    /// 消息发送基类
    /// </summary>
    public class BaseService
    {
        #region 字段
        public static IConnection _connection;
        #endregion

        #region 属性
        /// <summary>
        /// 服务器配置
        /// </summary>
        public RabbitMqConfigModel RabbitConfig { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public BaseService(RabbitMqConfigModel config)
        {
            try
            {
                RabbitConfig = config;
                CreateConn();
            }
            catch(BrokerUnreachableException bex)
            {
                throw bex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建连接
        /// </summary>
        public void CreateConn()
        {
            ConnectionFactory cf = new ConnectionFactory();
            cf.HostName = RabbitConfig.IP;
            cf.Port = RabbitConfig.Port;                                                      //服务器的端口
            cf.Endpoint = new AmqpTcpEndpoint(new Uri("amqp://" + RabbitConfig.IP + "/"));    //服务器ip
            cf.UserName = RabbitConfig.UserName;      //登录账户
            cf.Password = RabbitConfig.Password;      //登录账户
            cf.VirtualHost = RabbitConfig.VirtualHost;    //虚拟主机
            cf.RequestedHeartbeat = 60;     //虚拟主机


            _connection = cf.CreateConnection();
        }
        #endregion

        #region 方法

        #region 发送消息
        /// <summary>
        /// 发送消息，泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Send<T>(T messageInfo, ref string errMsg)
        {
            if (messageInfo == null)
            {
                errMsg = "消息对象不能为空";
                return false;
            }
            string value = JsonConvert.SerializeObject(messageInfo);
            return Send(value, ref errMsg);
        }
        /// <summary>
        /// 发送消息，string类型
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool Send(string message, ref string errMsg)
        {
            if (string.IsNullOrEmpty(message))
            {
                errMsg = "消息不能为空";
                return false;
            }
            try
            {
                if (!_connection.IsOpen)
                {
                    CreateConn();
                }
                using (var channel = _connection.CreateModel())
                {
                    //推送消息
                    byte[] bytes = Encoding.UTF8.GetBytes(message);

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = Convert.ToByte(RabbitConfig.DurableMessage ? 2 : 1);  //支持可持久化数据

                    if (!string.IsNullOrEmpty(RabbitConfig.ExchangeName))
                    {
                        //使用自定义的路由
                        channel.ExchangeDeclare(RabbitConfig.ExchangeName, RabbitConfig.ExchangeType.ToString(), RabbitConfig.DurableMessage, false, null);
                        channel.BasicPublish("", RabbitConfig.QueueName, properties, bytes);
                    }
                    else
                    {
                        //申明消息队列，且为可持久化的，如果队列的名称不存在，系统会自动创建，有的话不会覆盖
                        channel.QueueDeclare(RabbitConfig.QueueName, RabbitConfig.DurableQueue, false, false, null);
                        channel.BasicPublish(RabbitConfig.ExchangeName, RabbitConfig.RoutingKey, properties, bytes);
                    }
                    return true;
                }

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }
        #endregion

        #endregion
    }
}
