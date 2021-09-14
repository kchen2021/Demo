using RabbitMq.Consumer.Config;
using RabbitMq.Core.RabbitService;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RabbitMq.Consumer.Send
{
    class Program
    {
        private static Queue<string> queue = new Queue<string>();

        static void Main()
        {
            StartPush();


            Console.ReadLine();
        }


        private static void StartPush()
        {
            RabbitEventService mq = ConfigHelper.CreateDefaultInstance();

            for (int i = 0; i < 10000000; i++)
            {
                string errMsg = "";
                string body = $"Hello World!----{i}";

                mq.Send<string>(body, ref errMsg);

                Console.WriteLine("push:--------:" + body);
            }
               
        }
    }
}
