using System.Collections.Generic;

namespace Aloha.MessageBrokers
{
    public interface IContextProvider
    {
        string HeaderName { get; }
        object Get(IDictionary<string, object> headers);
    }
}
