using Aloha.MessageBrokers.RabbitMQ.Clients;
using Aloha.MessageBrokers.RabbitMQ.Contexts;
using Aloha.MessageBrokers.RabbitMQ.Conventions;
using Aloha.MessageBrokers.RabbitMQ.Initializers;
using Aloha.MessageBrokers.RabbitMQ.Internals;
using Aloha.MessageBrokers.RabbitMQ.Plugins;
using Aloha.MessageBrokers.RabbitMQ.Publishers;
using Aloha.MessageBrokers.RabbitMQ.Serializers;
using Aloha.MessageBrokers.RabbitMQ.Subscribers;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;

namespace Aloha.MessageBrokers.RabbitMQ
{
    public static class Extensions
    {
        private const string SectionName = "rabbitmq";
        private const string RegistryName = "messageBrokers.rabbitmq";

        public static IAlohaBuilder AddRabbitMq(this IAlohaBuilder builder, string sectionName = SectionName,
            Action<ConnectionFactory> connectionFactoryConfigurator = null)
        {
            builder.Services.AddSingleton<IContextProvider, ContextProvider>();

            builder.Services.AddSingleton<ICorrelationContextAccessor, CorrelationContextAccessor>();
            //builder.Services.AddSingleton<IMessagePropertiesAccessor>(new MessagePropertiesAccessor());
            builder.Services.AddSingleton<IConventionsBuilder, ConventionsBuilder>();
            builder.Services.AddSingleton<IConventionsProvider, ConventionsProvider>();
            builder.Services.AddSingleton<IConventionsRegistry, ConventionsRegistry>();
            builder.Services.AddSingleton<IRabbitMqSerializer, NewtonsoftJsonRabbitMqSerializer>();
            builder.Services.AddSingleton<IRabbitMqClient, RabbitMqClient>();
            builder.Services.AddSingleton<IBusPublisher, RabbitMqPublisher>();
            builder.Services.AddSingleton<IBusSubscriber, RabbitMqSubscriber>();
            builder.Services.AddSingleton<IRabbitMqPluginsExecutor, RabbitMqPluginsExecutor>();
            builder.Services.AddSingleton<RabbitMqExchangeInitializer>();
            builder.Services.AddSingleton<RabbitMqHostedService>();

            builder.AddInitializer<RabbitMqExchangeInitializer>();

            var options = builder.GetOptions<RabbitMqOptions>(sectionName);
            builder.Services.AddSingleton(options);

            var connectionFactory = new ConnectionFactory
            {
                Port = options.Port,
                VirtualHost = options.VirtualHost,
                UserName = options.Username,
                Password = options.Password,
                DispatchConsumersAsync = true                
            };

            connectionFactoryConfigurator?.Invoke(connectionFactory);
            var connection = connectionFactory.CreateConnection(options.HostNames, options.ConnectionName);
            builder.Services.AddSingleton(connection);

            return builder;
        }

        public static IBusSubscriber UseRabbitMq(this IServiceProvider serviceProvider)
           => new RabbitMqSubscriber(serviceProvider);
    }
}
