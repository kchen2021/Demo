using RabbitMq.Consumer.Config;
using RabbitMq.Core.RabbitService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Consumer.Receive
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitEventService mq = ConfigHelper.CreateDefaultInstance();
            mq.Receive(Write);

            Console.Read();
        }
        public static bool Write(string message)
        {
            Console.WriteLine("Received {0}", message);
            return  true;
        }
    }
}
