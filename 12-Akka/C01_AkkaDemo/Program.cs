using Akka.Actor;
using System;

namespace C01_AkkaDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Send();
            //MultiSend();

            Console.Read(); 
        }

        static void Send()
        {
            using var system = ActorSystem.Create("MySystem");
            var greeter = system.ActorOf<MessageActor>("greeter");

            greeter.Tell(new Message()
            {
                Id = Guid.NewGuid(),
                Name = "ZhangSan",
                Age = 12
            });
            greeter.Tell("test....");
            while (true) { }
        }

        static void MultiSend()
        {
            using var system = ActorSystem.Create("MySystem");
            var greeter = system.ActorOf<MultiActor>("greeter");

        }
    }


    

}
