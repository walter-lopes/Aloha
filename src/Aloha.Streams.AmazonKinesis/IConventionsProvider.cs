using System;

namespace Aloha.Streams.AmazonKinesis
{
    public interface IConventionsProvider
    {
        IConventions Get<T>();
        IConventions Get(Type type);
    }
}
