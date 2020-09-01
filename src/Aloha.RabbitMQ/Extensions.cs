using Aloha.MessageBrokers.RabbitMQ.Clients;
using Aloha.MessageBrokers.RabbitMQ.Contexts;
using Aloha.MessageBrokers.RabbitMQ.Conventions;
using Aloha.MessageBrokers.RabbitMQ.Initializers;
using Aloha.MessageBrokers.RabbitMQ.Internals;
using Aloha.MessageBrokers.RabbitMQ.Plugins;
using Aloha.MessageBrokers.RabbitMQ.Publishers;
using Aloha.MessageBrokers.RabbitMQ.Serializers;
using Aloha.MessageBrokers.RabbitMQ.Subscribers;
using DryIoc;
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
            builder.Container.Register<IContextProvider, ContextProvider>(reuse: Reuse.Singleton);

            builder.Container.Register<ICorrelationContextAccessor, CorrelationContextAccessor>(reuse: Reuse.Singleton);
            //builder.Services.AddSingleton<IMessagePropertiesAccessor>(new MessagePropertiesAccessor());
            builder.Container.Register<IConventionsBuilder, ConventionsBuilder>();
            builder.Container.Register<IConventionsProvider, ConventionsProvider>();
            builder.Container.Register<IConventionsRegistry, ConventionsRegistry>();
            builder.Container.Register<IRabbitMqSerializer, NewtonsoftJsonRabbitMqSerializer>();
            builder.Container.Register<IRabbitMqClient, RabbitMqClient>();
            builder.Container.Register<IBusPublisher, RabbitMqPublisher>();
            builder.Container.Register<IBusSubscriber, RabbitMqSubscriber>();
            builder.Container.Register<IRabbitMqPluginsExecutor, RabbitMqPluginsExecutor>();
            builder.Container.Register<RabbitMqExchangeInitializer>();
            builder.Container.Register<RabbitMqHostedService>();

            builder.AddInitializer<RabbitMqExchangeInitializer>();

            var options = builder.GetOptions<RabbitMqOptions>(sectionName);
            builder.Container.RegisterInstance(options);

            var connectionFactory = new ConnectionFactory
            {
                Port = options.Port,
                VirtualHost = options.VirtualHost,
                UserName = options.Username,
                Password = options.Password,
                //RequestedHeartbeat = options.RequestedHeartbeat,
                //RequestedConnectionTimeout = options.RequestedConnectionTimeout,
                //SocketReadTimeout = options.SocketReadTimeout,
                //SocketWriteTimeout = options.SocketWriteTimeout,
                //RequestedChannelMax = options.RequestedChannelMax,
                //RequestedFrameMax = options.RequestedFrameMax,
                //UseBackgroundThreadsForIO = options.UseBackgroundThreadsForIO,
                DispatchConsumersAsync = true,
                //ContinuationTimeout = options.ContinuationTimeout,
                //HandshakeContinuationTimeout = options.HandshakeContinuationTimeout,
                //NetworkRecoveryInterval = options.NetworkRecoveryInterval,
                
            };

            connectionFactoryConfigurator?.Invoke(connectionFactory);
            var connection = connectionFactory.CreateConnection(options.HostNames, options.ConnectionName);
            builder.Container.RegisterInstance(connection);

            return builder;
        }

        public static IBusSubscriber UseRabbitMq(this IContainer container)
           => new RabbitMqSubscriber(container);
    }
}
