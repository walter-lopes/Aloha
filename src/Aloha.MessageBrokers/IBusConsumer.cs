using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers
{
    public interface IBusConsumer
    {
        Task<IBusConsumer> Consume<T>(Func<IServiceProvider, IEnumerable<T>, object, Func<IEnumerable<T>, Task>, Task> handle) where T : class;
    }
}
