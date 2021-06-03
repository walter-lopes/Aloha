using Aloha.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers
{
    public interface IBusConsumer
    {
        Task<IBusConsumer> Consume<T>(Func<IServiceProvider, IEnumerable<T>, Task<IEnumerable<T>>> handle)
                where T : IIdentifiable<string>;
    }
}
