using Aloha.MessageBrokers.AmazonSQS.Clients;
using Aloha.MessageBrokers.AmazonSQS.Consumers;
using Aloha.MessageBrokers.AmazonSQS.Conventions;
using Aloha.MessageBrokers.AmazonSQS.Publishers;
using Aloha.Serializers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Aloha.MessageBrokers.AmazonSQS
{
    public static class Extensions
    {
        private const string SectionName = "amazonSQS";

        public static IAlohaBuilder AddAmazonSQS(this IAlohaBuilder builder, string sectionName = SectionName)
        {
            builder.Services.AddSingleton<ICorrelationContextAccessor, CorrelationContextAccessor>();
            builder.Services.AddTransient<IAmazonSQSClient, AmazonSQSClient>();
            builder.Services.AddScoped<IConventions, MessageConventions>();
            builder.Services.AddScoped<IConventionsProvider, ConventionsProvider>();          
            builder.Services.AddScoped<IBusPublisher, AmazonSQSPublisher>();
            builder.Services.AddScoped<IBusConsumer, AmazonSQSConsumer>();
            builder.Services.AddSingleton<IAlohaSerializer, NewtonsoftJsonAlohaSerializer>();

            var options = builder.GetOptions<AmazonSQSOptions>(sectionName);
            builder.Services.AddSingleton(options);

            return builder;
        }

        public static IBusConsumer UseAmazonSQS(this IServiceProvider serviceProvider)
            => new AmazonSQSConsumer(serviceProvider);
    }
}
