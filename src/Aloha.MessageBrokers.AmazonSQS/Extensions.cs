using Aloha.MessageBrokers.AmazonSQS.Clients;
using Aloha.MessageBrokers.AmazonSQS.Conventions;
using Aloha.MessageBrokers.AmazonSQS.Publishers;
using DryIoc;

namespace Aloha.MessageBrokers.AmazonSQS
{
    public static class Extensions
    {
        private const string SectionName = "amazonSQS";

        public static IAlohaBuilder AddAmazonSQS(this IAlohaBuilder builder, string sectionName = SectionName)
        {
            builder.Container.Register<IAmazonSQSClient, AmazonSQSClient>();
            builder.Container.Register<IConventions, MessageConventions>();
            builder.Container.Register<IConventionsProvider, ConventionsProvider>();          
            builder.Container.Register<IBusPublisher, AmazonSQSPublisher>();

            var options = builder.GetOptions<AmazonSQSOptions>(sectionName);
            builder.Container.RegisterInstance(options);

            return builder;
        }
    }
}
