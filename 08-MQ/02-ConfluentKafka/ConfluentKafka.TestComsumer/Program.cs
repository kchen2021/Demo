using ConfluentKafa.Interfaces;
using ConfluentKafka.Core.xKafkaConfluent;
using System;
using System.Threading;
using ConfluentKafa.Interfaces.Errors;
using ConfluentKafa.Interfaces.Extentions;
using ConfluentKafka.Core.xSerialization;
using xApplicationInsights.Monitoring.Interfaces;
using ConfluentKafka.Config;
using xApplicationInsights.Monitoring.Core;

namespace ConfluentKafka.TestComsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            IMonitorFactory monitorFactory = new MonitorFactory("prefix");
            var confluentConsumer =
                new ConfluentConsumer<int, int>(
                    ConfigSettings.TopicName,
                    ConfigSettings.Serializer,
                    ConfigSettings.Serializer,
                    ConfigSettings.GetConsumerSettings, 
                    monitorFactory);

            confluentConsumer.Start();

            confluentConsumer.ErrorAccured += SomethingError;
            confluentConsumer.MessageReceived += DoSomething;
            confluentConsumer.Start();

            Thread.Sleep(10000);
            Console.WriteLine();
            Console.WriteLine("Hello World!");
            

            confluentConsumer.Dispose();

            System.Threading.Thread.Sleep(-1);
            Console.Read();
        }
        private static void DoSomething(object obj, Message<int, int> message)
        {
            var received = false;
            Console.WriteLine($"getData: key:{message.Key}   value:{message.Value}");
        }


        private static void SomethingError(object obj, MessagingError error)
        {
            Console.WriteLine("SomethingError");
            Console.WriteLine(error.Message);
        }

    }
}
