using ConfluentKafka.Core.xSerialization;
using System;
using System.Threading;
using ConfluentKafka.Core.xKafkaConfluent;
using ConfluentKafka.Config;

namespace ConfluentKafka.TestProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var producer = new ConfluentProducer<int, int>(
                ConfigSettings.TopicName,
                ConfigSettings.ProducerSettings,
                ConfigSettings.Serializer,
                ConfigSettings.Serializer);
            
            var isSuccess = false;
            int i = 1000;
            while (i>= 0)
            {
                isSuccess = producer.SendAsync( i,  i).Wait(TimeSpan.FromSeconds(10));
                Console.WriteLine($"key:{ i}   value:{ i}");
                --i;
                Thread.Sleep(1000);
            }

            Console.WriteLine(isSuccess);

            Console.WriteLine();
            Console.WriteLine("Hello World!");

            Console.ReadLine();
        }
    }
}
