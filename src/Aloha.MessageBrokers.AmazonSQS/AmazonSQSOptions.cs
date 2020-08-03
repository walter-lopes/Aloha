﻿using System.Collections.Generic;

namespace Aloha.MessageBrokers.AmazonSQS
{
    public class AmazonSQSOptions
    {
        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public string Endpoint { get; set; }

        public IDictionary<string, string> Queues { get; set; }
    }
}
