using Akka.Actor;
using Akka.Configuration;
using C02_RemoteDemo.Client.Senders;
using C02_RemoteDemo.Common;
using C02_RemoteDemo.Common.Objects;
using System;

namespace C02_RemoteDemo.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"
akka {  
    actor{
        provider = remote
        deployment {
            /remoteecho {
                remote = ""akka.tcp://MyServer@localhost:8090""
            }
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
                //deploy remotely via config
                //var remoteEcho1 = system.ActorOf(Props.Create(() => new EchoActor()), "remoteecho");
                //system.ActorOf(Props.Create(() => new HelloActor(remoteEcho1)));



                //deploy remotely via code
                //var remoteAddress = Address.Parse("akka.tcp://DeployTarget@localhost:8090");
                //var remoteEcho2 =
                //    system.ActorOf(
                //        Props.Create(() => new EchoActor())
                //            .WithDeploy(Deploy.None.WithScope(new RemoteScope(remoteAddress))), "coderemoteecho");
                //system.ActorOf(Props.Create(() => new HelloActor(remoteEcho2)));




                //var remoteEcho3 = system.ActorOf(Props.Create(() => new Sender()), "remoteecho");
                //remoteEcho3.Tell(new Hello("test..."));

                //var remoteAddress = Address.Parse("akka.tcp://DeployTarget@localhost:8090/user/ChatServer");
                //var remoteEcho4 =
                //    system.ActorOf(
                //        Props.Create(() => new Sender())
                //            .WithDeploy(Deploy.None.WithScope(new RemoteScope(remoteAddress))), "coderemoteecho");
                //remoteEcho4.Tell(new Hello("test..."));



                //var chatClient = system.ActorOf(Props.Create<SenderActor>());
                //chatClient.Tell("test...");

                //var remoteAddress = Address.Parse(Program.Source);
                //var remoteEcho4 = system.ActorOf(Props.Create(() => new SenderActor1()).WithDeploy(Deploy.None.WithScope(new RemoteScope(remoteAddress))));
                //var remoteEcho3 = system.ActorOf(Props.Create(() => new SenderActor1()), "remoteecho");


                //Sender.Send();
                TimingSender.Send();

                Console.ReadKey();
            }
        }
    }

    public class SenderActor1 : ReceiveActor, ILogReceive
    {
        public SenderActor1()
        {
            Sender.Tell(new Hello("test..."));
        }

        //: UntypedActor, ILogReceive
        //protected override void OnReceive(object message)
        //    {
        //        Sender.Tell(message);
        //    }
    }
}
