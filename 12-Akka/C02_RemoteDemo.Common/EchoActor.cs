//using Akka.Actor;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace C02_RemoteDemo.Common
//{
//    public class EchoActor : ReceiveActor
//    {
//        /// <summary>
//        /// Echo 发出
//        /// </summary>
//        public EchoActor()
//        {
//            Receive<Hello>(hello =>
//            {
//                Console.WriteLine("[{0}]: {1}", Sender, hello.Message);
//                Sender.Tell(hello);
//            });
//        }
//    }
//}
