using Aloha.MessageBrokers;
using Aloha.RabbitMQ.Clients;
using Aloha.RabbitMQ.Contexts;
using Aloha.RabbitMQ.Conventions;
using Aloha.RabbitMQ.Initializers;
using Aloha.RabbitMQ.Internals;
using Aloha.RabbitMQ.Plugins;
using Aloha.RabbitMQ.Publishers;
using Aloha.RabbitMQ.Serializers;
using Aloha.RabbitMQ.Subscribers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;

namespace Aloha.RabbitMQ
{
    public static class Extensions
    {
        private const string SectionName = "rabbitmq";
        private const string RegistryName = "messageBrokers.rabbitmq";

        public static IAlohaBuilder AddRabbitMq(this IAlohaBuilder builder, string sectionName = SectionName,
            Action<ConnectionFactory> connectionFactoryConfigurator = null)
        {
            builder.Services.AddSingleton<IContextProvider, ContextProvider>();
            //builder.Services.AddSingleton<ICorrelationContextAccessor>(new CorrelationContextAccessor());
            //builder.Services.AddSingleton<IMessagePropertiesAccessor>(new MessagePropertiesAccessor());
            builder.Services.AddSingleton<IConventionsBuilder, ConventionsBuilder>();
            builder.Services.AddSingleton<IConventionsProvider, ConventionsProvider>();
            builder.Services.AddSingleton<IConventionsRegistry, ConventionsRegistry>();
            builder.Services.AddSingleton<IRabbitMqSerializer, NewtonsoftJsonRabbitMqSerializer>();
            builder.Services.AddSingleton<IRabbitMqClient, RabbitMqClient>();
            builder.Services.AddSingleton<IBusPublisher, RabbitMqPublisher>();
            builder.Services.AddSingleton<IBusSubscriber, RabbitMqSubscriber>();
            builder.Services.AddSingleton<IRabbitMqPluginsExecutor, RabbitMqPluginsExecutor>();
            builder.Services.AddTransient<RabbitMqExchangeInitializer>();
            builder.Services.AddHostedService<RabbitMqHostedService>();
            builder.AddInitializer<RabbitMqExchangeInitializer>();

            var options = builder.GetOptions<RabbitMqOptions>(sectionName);
            builder.Services.AddSingleton(options);
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
            builder.Services.AddSingleton(connection);

            return builder;
        }

        public static IBusSubscriber UseRabbitMq(this IApplicationBuilder app)
           => new RabbitMqSubscriber(app.ApplicationServices);
    }
}
