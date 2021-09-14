using ConfluentKafka.Core.xSerialization;
using System;
using System.Collections.Generic;

namespace ConfluentKafka.Config
{
    public class ConfigSettings
    {
        public const string TopicName = "kchen"; 
        public static readonly string GroupId = $"{Environment.MachineName}-{TopicName}";

        public  const  string BootstrapServers = "10.4.0.9:9092,10.4.0.9:9093,10.4.0.9:9094";

        public static IReadOnlyDictionary<string, object>  ProducerSettings => new Dictionary<string, object>
        {
            {"bootstrap.servers", BootstrapServers},
            // {"debug", "protocol" },
            {"socket.blocking.max.ms", "1"}, //min=1
            {"queue.buffering.max.ms", "0"},
            //{ "num.partitions", 3},
            //set to 0 for immediate transmission, or some other low reasonable value (e.g. 5 ms)
            {
                "default.topic.config", new Dictionary<string, object>
                {
                    {"acks", "1"} //1 by default, globally as opposed to per-topic
                }
            }
        };


        public static Dictionary<string, object> GetConsumerSettings =>
            new Dictionary<string, object>
            {
                {"client.id", TopicName},
                {"enable.auto.commit", true},
                {"group.id", GroupId},
                {"bootstrap.servers", BootstrapServers},
                {"auto.offset.reset", "earliest"},
                //Consumer batch latency -fetch.wait.max.ms - how much time the consumer gives the broker to fill up fetch.min.bytes worth of messages before responding.
                //Setting fetch.wait.max.ms too low (lower than the partition message rate) causes the occasional FetchRequest to return empty before any new messages were seen on the broker, this in turn kicks in the fetch.error.backoff.ms timer that waits that long before the next FetchRequest. So you might want to decrease fetch.error.backoff.ms too.
                //https://github.com/edenhill/librdkafka/wiki/How-to-decrease-message-latency
                {"fetch.wait.max.ms", "0"},
                {"fetch.error.backoff.ms", "0"},
                {"socket.blocking.max.ms", "1"}, //min=1
                //{ "num.partitions", 3},
                {
                    "default.topic.config", new Dictionary<string, object>
                    {
                        {"auto.offset.reset", "earliest"}
                    }
                }

            };


        public static readonly Serializer<int> Serializer = new Serializer<int>(
            BitConverter.GetBytes,
            bytes => BitConverter.ToInt32(bytes, 0)
        );

    }
}
