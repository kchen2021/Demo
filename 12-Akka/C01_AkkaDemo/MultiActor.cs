using Akka.Actor;
using Akka.DI.Core;
using Akka.Routing;

namespace C01_AkkaDemo
{
    public class MultiActor: ReceiveActor
    {
        public MultiActor()
        {
            var props =  Context.DI().Props<MessageActor>().WithRouter(new SmallestMailboxPool(2));
            var actorRef =  Context.ActorOf(props, "1111111111111111111111111111");
            actorRef.Tell("222222222222222222");
        }
    }
}
