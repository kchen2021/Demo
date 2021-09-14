using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace C01_AkkaDemo
{
    public class MessageActor : ReceiveActor
    {
        private readonly Cancelable _cancelabe;
        private Task task;

        public MessageActor()
        {
            _cancelabe = new Cancelable(Context.System.Scheduler);

            Receive<Message>(message =>
            {
                Console.WriteLine("MessageActor:" + message.ToString());
            });
            Receive<string>(message =>
            {
                Console.WriteLine("MessageActor:" + message);
            });
        }

        protected override void PreRestart(Exception reason, object message)
        {
            base.PreRestart(reason, message);
        }

        protected override void PreStart()
        {
            Context.System.Scheduler.ScheduleTellRepeatedly(TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1),
                Self,
                new Message()
                {
                    Id = Guid.NewGuid(),
                    Name = "lisi",
                    Age = DateTime.Now.Second
                },
                ActorRefs.Nobody,
                _cancelabe);

            base.PreStart();
        }

        protected override void PostStop()
        {
            //_cancelabe.Cancel();
            //base.PostStop();
        }
    }
}
