using System;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers
{
    public interface IBusSubscriber
    {
        IBusSubscriber Subscribe<T>(Func<IServiceProvider, T, object, Task> handle) where T : class;
    }
}
