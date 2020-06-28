using RabbitMQ.Client.Events;
using System;
using System.Threading.Tasks;

namespace Aloha.RabbitMQ
{
    internal interface IRabbitMqPluginsExecutor
    {
        Task ExecuteAsync(Func<object, object, BasicDeliverEventArgs, Task> successor,
          object message, object correlationContext, BasicDeliverEventArgs args);
    }
}
