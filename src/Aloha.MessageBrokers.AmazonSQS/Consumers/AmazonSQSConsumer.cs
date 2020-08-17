using DryIoc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers.AmazonSQS.Consumers
{
    public class AmazonSQSConsumer : IBusConsumer
    {
        private readonly IContainer _container;
        private readonly IAmazonSQSClient _amazonSQSClient;
        private readonly IConventionsProvider _conventions;

        public AmazonSQSConsumer(IContainer container)
        {
            _container = container;
            _amazonSQSClient = _container.GetRequiredService<IAmazonSQSClient>();
            _conventions = _container.GetRequiredService<IConventionsProvider>();
        }

        public async Task<IBusConsumer> Consume<T>(Func<IServiceProvider, IEnumerable<T>, object, Func<IEnumerable<T>, Task>, Task> handle) where T : class
        {
            var queue = _conventions.Get<T>().Queue;

            var entries = await _amazonSQSClient.ReceiveMessageAsync<T>(queue);

            await handle(_container, entries.Select(x => x.Message), null, ProcessErrorMessage);

            return this;
        }


        private Task ProcessErrorMessage<T>(IEnumerable<T> errors) where T : class
        {
            return Task.CompletedTask;
        }
    }
}
