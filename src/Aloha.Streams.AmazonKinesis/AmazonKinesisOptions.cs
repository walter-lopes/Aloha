using System;
using System.Collections.Generic;

namespace Aloha.Streams.AmazonKinesis
{
    public class AmazonKinesisOptions
    {
        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public string Region { get; set; }

        public IDictionary<string, string> Streams { get; set; }
    }
}
