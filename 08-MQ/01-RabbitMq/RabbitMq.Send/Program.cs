using RabbitMq.Core.RabbitService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMq.Core.Model;

namespace RabbitMq.Send
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();

            Console.ReadKey();
        }

        private static void Test()
        {
            RabbitBasicService mq = new RabbitBasicService(new RabbitMqConfigModel()
            {
                IP = "47.104.111.126",
                //UserName = "guest",
                //Password = "guest",
                UserName = "kevin",
                Password = "5611",
                Port = 15672,
                VirtualHost = "kevinHost",
                DurableQueue = true,
                ExchangeName = "ex1",
                ExchangeType = ExchangeTypeEnum.direct,
                DurableMessage = true
            });

            for (int i = 0; i < 10000000; i++)
            {
                int index = new Random().Next(1, 4);
                switch (index)
                {
                    case 1:
                        mq.RabbitConfig.QueueName = "error";
                        mq.RabbitConfig.RoutingKey = "system.error";
                        break;
                    case 2:
                        mq.RabbitConfig.QueueName = "info";
                        mq.RabbitConfig.RoutingKey = "system.info";
                        break;
                    case 3:
                        mq.RabbitConfig.QueueName = "bug";
                        mq.RabbitConfig.RoutingKey = "system.bug";
                        break;
                }

                string value = i.ToString() + "  ExchangeName=" + mq.RabbitConfig.ExchangeName + "   ExchangeType=" + mq.RabbitConfig.ExchangeType.ToString() + "     RoutingKey=" + mq.RabbitConfig.RoutingKey;
                string errMsg = "";
                mq.Send<string>(value, ref errMsg);
                Console.WriteLine("消息已发送：" + value);
            }
        }
    }
}
