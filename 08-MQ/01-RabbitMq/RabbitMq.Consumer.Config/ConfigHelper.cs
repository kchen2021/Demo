using RabbitMq.Core.RabbitService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMq.Core.Model;

namespace RabbitMq.Consumer.Config
{
    public class ConfigHelper
    {
            public static RabbitEventService CreateDefaultInstance()
            {
                //return new RabbitEventService(new RabbitMqConfigModel()
                //{
                //    //IP = "127.0.0.1",
                //    IP = "47.104.111.126",
                //    UserName = "kevin",
                //    Password = "5611",
                //    Port = 15672,
                //    VirtualHost = "kevinHost",
                //    DurableQueue = true,
                //    QueueName = "hello",
                //    ExchangeName = "hello_ex1",
                //    ExchangeType = ExchangeTypeEnum.direct,
                //    DurableMessage = false,
                //    RoutingKey = "bug"
                //});

                //return new RabbitEventService(new RabbitMqConfigModel()
                //{
                //    //IP = "127.0.0.1",
                //    IP = "47.104.111.126",
                //    UserName = "kevin",
                //    Password = "5611",
                //    Port = 15672,
                //    VirtualHost = "kevinHost",
                //    DurableQueue = true,
                //    QueueName = "error",
                //    ExchangeName = "ex1",
                //    ExchangeType = ExchangeTypeEnum.direct,
                //    DurableMessage = false,
                //    RoutingKey = "system.error"
                //});




                return new RabbitEventService(new RabbitMqConfigModel()
                {
                    //IP = "127.0.0.1",
                    IP = "47.98.227.240",
                    UserName = "kevin",
                    Password = "5611",
                    Port = 15672,
                    VirtualHost = "kevinHost",
                    DurableQueue = true,
                    QueueName = "error",
                    ExchangeName = "ex1",
                    ExchangeType = ExchangeTypeEnum.direct,
                    DurableMessage = false,
                    RoutingKey = "system.error"
                });
            }
    }
}
