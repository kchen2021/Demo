using RabbitMq.Core.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Core.RabbitService
{
    public class RabbitEventService: BaseService
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public RabbitEventService(RabbitMqConfigModel config)
            : base(config)
        {
        }
        #endregion

        #region 方法

        #region 接收消息
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="method"></param>
        public void Receive(Func<string, bool> method)
        {
            try
            {
                using (var channel = _connection.CreateModel())
                {
                    //申明队列
                    channel.QueueDeclare(RabbitConfig.QueueName, RabbitConfig.DurableQueue, false, false, null);

                    if (string.IsNullOrEmpty(RabbitConfig.ExchangeName)) //若交换机不为空
                    {
                        //申明路由
                        channel.ExchangeDeclare(RabbitConfig.ExchangeName, RabbitConfig.ExchangeType.ToString(), RabbitConfig.DurableQueue);
                        //队列和交换机绑定
                        channel.QueueBind(RabbitConfig.QueueName, RabbitConfig.ExchangeName, RabbitConfig.RoutingKey);
                    }

                    //channel.BasicQos(0, 1, false);

                    var consumer = new EventingBasicConsumer(channel);
                    
                    //注册接收事件，一旦创建连接就去拉取消息
                    consumer.Received += (model, ea) =>
                    {
                        int resultType = 0;
                        try
                        {
                            var body = ea.Body;
                            string message = Encoding.UTF8.GetString(body);
                            method(message);
                            resultType = 1;
                        }
                        catch(Exception)
                        {
                            resultType = -1;
                        }
                        finally
                        {
                            switch (resultType)
                            {
                                case -1:
                                    channel.BasicNack(ea.DeliveryTag, false, true);//执行失败，重新插入到消息队列中
                                    break;
                                case 0:
                                    channel.BasicNack(ea.DeliveryTag, false, false);//执行失败，但不重新插入消息队列中
                                    break;
                                case 1:
                                    channel.BasicAck(ea.DeliveryTag, false); //执行成功
                                    break;
                            }
                        }                        
                    };
                    channel.BasicConsume(RabbitConfig.QueueName,
                                         false,//和tcp协议的ack一样，为false则服务端必须在收到客户端的回执（ack）后才能删除本条消息
                                         consumer);
                    System.Threading.Thread.Sleep(-1);
                  
                }
            }
            catch (Exception)
            {
            }            
        }
        #endregion
        
        #endregion
    }
}
