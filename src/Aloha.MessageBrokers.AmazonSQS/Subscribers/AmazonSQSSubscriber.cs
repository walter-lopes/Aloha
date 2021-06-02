using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers.AmazonSQS.Subscribers
{
    public class AmazonSQSSubscriber : IBusSubscriber
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAmazonSQSClient _amazonSQSClient;
        private readonly IConventionsProvider _conventions;

        public AmazonSQSSubscriber(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _amazonSQSClient = _serviceProvider.GetRequiredService<IAmazonSQSClient>();
            _conventions = _serviceProvider.GetRequiredService<IConventionsProvider>();
        }

        public async Task<IBusSubscriber> Subscribe<T>(Func<IServiceProvider, T, object, Task> handle) where T : class
        {
            var entries = await _amazonSQSClient.ReceiveMessageAsync<T>(_conventions.Get<T>().Queue, 1);

            var message = entries.FirstOrDefault().Message;

            await handle(_serviceProvider, message, null);

            return this;
        }
    }
}
