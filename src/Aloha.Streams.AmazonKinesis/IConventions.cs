using System;

namespace Aloha.Streams.AmazonKinesis
{
    public interface IConventions
    {
        Type Type { get; }

        string StreamName { get; }
    }
}
