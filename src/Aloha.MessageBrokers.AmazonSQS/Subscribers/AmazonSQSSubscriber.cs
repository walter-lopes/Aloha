using DryIoc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers.AmazonSQS.Subscribers
{
    public class AmazonSQSSubscriber : IBusSubscriber
    {
        private readonly IContainer _container;
        private readonly IAmazonSQSClient _amazonSQSClient;
        private readonly IConventionsProvider _conventions;

        public AmazonSQSSubscriber(IContainer container)
        {
            _container = container;
            _amazonSQSClient = _container.GetRequiredService<IAmazonSQSClient>();
            _conventions = _container.GetRequiredService<IConventionsProvider>();
        }

        public async Task<IBusSubscriber> Subscribe<T>(Func<IServiceProvider, T, object, Task> handle) where T : class
        {
            var entries = await _amazonSQSClient.ReceiveMessageAsync<T>(_conventions.Get<T>().Queue);

            var message = entries.FirstOrDefault().Message;

            await handle(_container, message, null);

            return this;
        }
    }
}
