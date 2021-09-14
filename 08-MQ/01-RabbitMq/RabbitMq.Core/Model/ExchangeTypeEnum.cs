using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Core.Model
{
    /// <summary>
    /// 交换机交换类型
    /// </summary>
    public enum  ExchangeTypeEnum
    {
        /// <summary>
        /// 不处理路由键。你只需要简单的将队列绑定到交换机上。一个发送到交换机的消息都会被转发到与该交换机绑定的所有队列上。
        /// 很像子网广播，每台子网内的主机都获得了一份复制的消息。Fanout交换机转发消息是最快的。
        /// </summary>
        fanout = 1,

        /// <summary>
        /// 处理路由键。需要将一个队列绑定到交换机上，要求该消息与一个特定的路由键完全匹配
        /// 。这是一个完整的匹配。如果一个队列绑定到该交换机上要求路由键 “dog”，
        /// 则只有被标记为“dog”的消息才被转发，不会转发dog.puppy，也不会转发dog.guard，只会转发dog。 
        /// </summary>
        direct = 2,

        /// <summary>
        /// 将路由键和某模式进行匹配。此时队列需要绑定要一个模式上。
        /// 符号“#”匹配一个或多个词，符号“*”匹配不多不少一个词。
        /// 因此“audit.#”能够匹配到“audit.irs.corporate”，但是“audit.*” 只会匹配到“audit.irs”
        /// </summary>
        topic = 3,

        header = 4
    }
}
