using Aloha.MessageBrokers;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers.RabbitMQ.Clients
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private readonly IRabbitMqSerializer _serializer;
        private readonly IContextProvider _contextProvider;
        private readonly RabbitMqOptions _options;
        private readonly ILogger<RabbitMqClient> _logger;
        private readonly IModel _channel;

        public RabbitMqClient(IConnection connection, IRabbitMqSerializer serializer, IContextProvider contextProvider,
            RabbitMqOptions options, ILogger<RabbitMqClient> logger)
        {
            _serializer = serializer;
            _contextProvider = contextProvider;
            _options = options;
            _logger = logger;
            _channel = connection.CreateModel();
        }

        public Task Send(object message, IConventions conventions, string messageId = null, string correlationId = null,
            string spanContext = null, object messageContext = null, IDictionary<string, object> headers = null)
        {
            var payload = _serializer.Serialize(message);

            var body = Encoding.UTF8.GetBytes(payload);

            IBasicProperties properties = CreateProperties(messageId, correlationId, headers);

            _channel.BasicPublish(conventions.Exchange, conventions.RoutingKey, properties, body);

            return Task.CompletedTask;
        }

        private IBasicProperties CreateProperties(string messageId, string correlationId, IDictionary<string, object> headers)
        {
            var properties = _channel.CreateBasicProperties();

            properties.MessageId = string.IsNullOrWhiteSpace(messageId)
                ? Guid.NewGuid().ToString("N")
                : messageId;

            properties.CorrelationId = string.IsNullOrWhiteSpace(correlationId)
                ? Guid.NewGuid().ToString("N")
                : correlationId;

            properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            properties.Headers = new Dictionary<string, object>();

            if (headers is { })
            {
                foreach (var (key, value) in headers)
                {
                    if (string.IsNullOrWhiteSpace(key) || value is null)
                    {
                        continue;
                    }

                    properties.Headers.TryAdd(key, value);
                }
            }

            return properties;
        }
    }
}
