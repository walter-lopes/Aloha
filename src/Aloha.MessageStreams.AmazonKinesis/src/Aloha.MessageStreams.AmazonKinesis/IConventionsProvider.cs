using System;

namespace Aloha.MessageStreams.AmazonKinesis
{
    public interface IConventionsProvider
    {
        IConventions Get<T>();
        IConventions Get(Type type);
    }
}
