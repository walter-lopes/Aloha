using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers.RabbitMQ.Plugins
{
    internal sealed class RabbitMqPluginsExecutor : IRabbitMqPluginsExecutor
    {
      //  private readonly IRabbitMqPluginsRegistryAccessor _registry;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMqPluginsExecutor(IServiceProvider serviceProvider)
        {
          //  _registry = registry;
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync(Func<object, object, BasicDeliverEventArgs, Task> successor,
            object message, object correlationContext, BasicDeliverEventArgs args)
        {

            await successor(message, correlationContext, args);
            return;
        }
    }
}
