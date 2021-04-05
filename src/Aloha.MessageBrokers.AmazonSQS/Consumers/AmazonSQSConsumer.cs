using Aloha.MessageBrokers.AmazonSQS.Messages;
using Aloha.Types;
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

        private readonly AmazonSQSOptions _options;

        public AmazonSQSConsumer(IContainer container)
        {
            _container = container;
            _amazonSQSClient = _container.GetRequiredService<IAmazonSQSClient>();
            _conventions = _container.GetRequiredService<IConventionsProvider>();
            _options = _container.GetRequiredService<AmazonSQSOptions>();
        }

        public async Task<IBusConsumer> Consume<T>(Func<IServiceProvider, IEnumerable<T>, Task<IEnumerable<T>>> handle) 
                where T : IIdentifiable<string>
        {
            try
            {
                var queue = _conventions.Get<T>().Queue;
                int itemsToConsume = _options.ItemsToConsume;

                if (itemsToConsume <= 0)
                {
                    throw new Exception("Items to consume is invalid, please configure a valid number.");
                }

                var entryMessages = new List<MessageEntry<T>>(itemsToConsume);
                IEnumerable<MessageEntry<T>> messages;

                do
                {
                    messages = await _amazonSQSClient.ReceiveMessageAsync<T>(queue, 10);
                    entryMessages.AddRange(messages);

                } while (entryMessages.Count < itemsToConsume && messages.Any());

                var incomingMessages = entryMessages.Select(x => x.Message);

                var results = await handle(_container, incomingMessages);

                var successMessageIds = new HashSet<string>(results.Select(x => x.Id));

                IDictionary<string, MessageEntry<T>> entryMessageDictionary = entryMessages.ToDictionary(x => x.UniqueKey);

                var succeededMessages = new List<MessageEntry<T>>(successMessageIds.Count());

                foreach (var id in successMessageIds)
                {
                    MessageEntry<T> message = entryMessageDictionary[id];
                    succeededMessages.Add(message);
                }

                await _amazonSQSClient.DeleteMessagesAsync(succeededMessages, queue);

                return this;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
