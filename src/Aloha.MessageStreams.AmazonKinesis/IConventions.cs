using System;

namespace Aloha.MessageStreams.AmazonKinesis
{
    public interface IConventions
    {
        Type Type { get; }

        string StreamName { get; }
    }
}
