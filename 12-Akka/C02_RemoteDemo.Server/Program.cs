using Akka.Actor;
using Akka.Configuration;
using C02_RemoteDemo.Common.Objects;
using System;

namespace C02_RemoteDemo.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"
akka {  
    actor.provider = remote
    remote {
        dot-netty.tcp {
            port = 8090
            hostname = localhost
        }
    }
}");
            using (var system = ActorSystem.Create("MyServer", config))
            {
                var greeter = system.ActorOf(Props.Create(()=>new Processor()), "ChatServer");

                Console.ReadKey();
            }
        }
    }


    public class Processor : ReceiveActor, ILogReceive
    {
        public Processor()
        {
            Receive<Hello>(a=>
            {
                Console.WriteLine(a.Message);
            });
        }
    }
}
