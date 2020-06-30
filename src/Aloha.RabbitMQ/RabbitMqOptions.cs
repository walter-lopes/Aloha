using System.Collections;
using System.Collections.Generic;

namespace Aloha.MessageBrokers.RabbitMQ
{
    public class RabbitMqOptions
    {
        public string ConnectionName { get; set; }

        public IList<string> HostNames { get; set; }

        public int Port { get; set; }

        public string VirtualHost { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public ExchangeOptions Exchange { get; set; }

        public QueueOptions Queue { get; set; }

        public ContextOptions Context { get; set; }

        public string ConventionsCasing { get; set; }

        public int Retries { get; set; }

        public int RetryInterval { get; set; }

        public QosOptions Qos { get; set; }

        public class QueueOptions
        {
            public string Template { get; set; }
            public bool Declare { get; set; }
            public bool Durable { get; set; }
            public bool Exclusive { get; set; }
            public bool AutoDelete { get; set; }
        }

        public class ExchangeOptions
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public bool Declare { get; set; }
            public bool Durable { get; set; }
            public bool AutoDelete { get; set; }
        }

        public class QosOptions
        {
            public uint PrefetchSize { get; set; }
            public ushort PrefetchCount { get; set; }
            public bool Global { get; set; }
        }

        public class ContextOptions
        {
            public bool Enabled { get; set; }
            public string Header { get; set; }
        }
    }
}
