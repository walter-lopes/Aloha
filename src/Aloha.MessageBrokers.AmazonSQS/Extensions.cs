using Aloha.MessageBrokers.AmazonSQS.Clients;
using Aloha.MessageBrokers.AmazonSQS.Consumers;
using Aloha.MessageBrokers.AmazonSQS.Conventions;
using Aloha.MessageBrokers.AmazonSQS.Publishers;
using Aloha.MessageBrokers.AmazonSQS.Subscribers;
using Aloha.Serializers;
using DryIoc;

namespace Aloha.MessageBrokers.AmazonSQS
{
    public static class Extensions
    {
        private const string SectionName = "amazonSQS";

        public static IAlohaBuilder AddAmazonSQS(this IAlohaBuilder builder, string sectionName = SectionName)
        {
            builder.Container.Register<ICorrelationContextAccessor, CorrelationContextAccessor>(reuse: Reuse.Singleton);
            builder.Container.Register<IAmazonSQSClient, AmazonSQSClient>();
            builder.Container.Register<IConventions, MessageConventions>();
            builder.Container.Register<IConventionsProvider, ConventionsProvider>();          
            builder.Container.Register<IBusPublisher, AmazonSQSPublisher>();
            builder.Container.Register<IBusConsumer, AmazonSQSConsumer>();
            builder.Container.Register<IAlohaSerializer, NewtonsoftJsonAlohaSerializer>();

            var options = builder.GetOptions<AmazonSQSOptions>(sectionName);
            builder.Container.RegisterInstance(options);

            return builder;
        }

        public static IBusSubscriber UseAmazonSQS(this IContainer container)
            => new AmazonSQSSubscriber(container);
    }
}
