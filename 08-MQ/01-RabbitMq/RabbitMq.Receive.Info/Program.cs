using RabbitMq.Core.RabbitService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMq.Core.Model;

namespace RabbitMq.Receive.Info
{
    class Program
    {
        static void Main(string[] args)
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
                QueueName = "info",
                ExchangeName = "ex1",
                ExchangeType = ExchangeTypeEnum.direct,
                DurableMessage = true,
                RoutingKey = "system.info"
            });

            Program p = new Program();
            mq.Receive<string>(p.GetMessage);


            Console.Read();
        }

        public void GetMessage(string value)
        {
            try
            {
                Console.WriteLine("接受到数据：" + value);
            }
            catch (Exception)
            {

            }
        }

    }
}
