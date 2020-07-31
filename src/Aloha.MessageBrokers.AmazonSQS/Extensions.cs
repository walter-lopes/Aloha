using Aloha.MessageBrokers.AmazonSQS.Clients;
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

            var options = builder.GetOptions<AmazonSQSOptions>(sectionName);
            builder.Container.RegisterInstance(options);

            builder.Container.Register<IBusPublisher, AmazonSQSPublisher>();

            return builder;
        }
    }
}
