using Akka.Actor;
using Akka.Configuration;
using C02_RemoteDemo.Common.Objects;
using System.Threading;

namespace C02_RemoteDemo.Client.Senders
{
    public class Sender
    {
        public static void Send()
        {
            var config = ConfigurationFactory.ParseString(@"
akka {  
    actor{
        provider = remote
    }
    remote {
        dot-netty.tcp {
            port = 0
            hostname = localhost
        }
    }
}");

            using (var system = ActorSystem.Create("Deployer", config))
            {
                var chatClient = system.ActorOf(Props.Create<SenderActor>());
                chatClient.Tell("test...");
            }
        }


        private class SenderActor : ReceiveActor, ILogReceive
        {
            public const string Source = "akka.tcp://MyServer@localhost:8090/user/ChatServer";
            private readonly ActorSelection _server = Context.ActorSelection(Source);
            public SenderActor()
            {
                Receive<string>(h =>
                {
                    _server.Tell(new Hello(h), Self);
                    Thread.Sleep(1000);
                });
            }
        }
    }
}
