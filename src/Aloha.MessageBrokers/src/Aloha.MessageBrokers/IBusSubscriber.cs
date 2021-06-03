using System;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers
{
    public interface IBusSubscriber
    {
        Task<IBusSubscriber> Subscribe<T>(Func<IServiceProvider, T, object, Task> handle) where T : class;
    }
}
