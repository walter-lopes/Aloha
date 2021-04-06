using Aloha.MessageBrokers.AmazonSQS.Clients;
using Aloha.MessageBrokers.AmazonSQS.Consumers;
using Aloha.MessageBrokers.AmazonSQS.Conventions;
using Aloha.MessageBrokers.AmazonSQS.Publishers;
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
            builder.Container.Register<IAmazonSQSClient, AmazonSQSClient>(reuse: Reuse.Transient);
            builder.Container.Register<IConventions, MessageConventions>(reuse: Reuse.Scoped);
            builder.Container.Register<IConventionsProvider, ConventionsProvider>(reuse: Reuse.Scoped);          
            builder.Container.Register<IBusPublisher, AmazonSQSPublisher>(reuse: Reuse.Scoped);
            builder.Container.Register<IBusConsumer, AmazonSQSConsumer>(reuse: Reuse.Scoped);
            builder.Container.Register<IAlohaSerializer, NewtonsoftJsonAlohaSerializer>(reuse: Reuse.Singleton);

            var options = builder.GetOptions<AmazonSQSOptions>(sectionName);
            builder.Container.RegisterInstance(options);

            return builder;
        }

        public static IBusConsumer UseAmazonSQS(this IContainer container)
            => new AmazonSQSConsumer(container);
    }
}
