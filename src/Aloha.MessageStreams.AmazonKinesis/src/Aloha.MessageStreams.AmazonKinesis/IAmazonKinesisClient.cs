﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aloha.MessageStreams.AmazonKinesis
{
    public interface IAmazonKinesisClient
    {
        Task<bool> RecordAsync<T>(T @event, string streamName);

        Task<bool> RecordAsync<T>(IEnumerable<T> events, string streamName);
    }
}
