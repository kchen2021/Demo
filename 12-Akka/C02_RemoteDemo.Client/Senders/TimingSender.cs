using Akka.Actor;
using Akka.Configuration;
using C02_RemoteDemo.Common.Objects;
using System;

namespace C02_RemoteDemo.Client.Senders
{
    public class TimingSender
    {
        public const string Source = "akka.tcp://MyServer@localhost:8090/user/ChatServer";
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
                var chatClient = system.ActorOf(Props.Create<TimingSenderActor>());
                chatClient.Tell("test...");
            }

        }



        private class SayHello { }
        private class TimingSenderActor : ReceiveActor
        {
            private int _helloCounter;
            private ICancelable _helloTask;

            public const string Source = "akka.tcp://MyServer@localhost:8090/user/ChatServer";
            private readonly ActorSelection _server = Context.ActorSelection(Source);
            public TimingSenderActor()
            {
                Receive<Hello>(hello =>
                {
                    Console.WriteLine("Received {1} from {0}", Sender, hello.Message);
                });

                Receive<SayHello>(sayHello =>
                {
                    _server.Tell(new Hello("hello" + _helloCounter++), Self);
                });
            }

            protected override void PreStart()
            {
                _helloTask = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(1), Context.Self, new SayHello(), ActorRefs.NoSender);
            }

            protected override void PostStop()
            {
                _helloTask.Cancel();
            }
        }
    }

    
}
