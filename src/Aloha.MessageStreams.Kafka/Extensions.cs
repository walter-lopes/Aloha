using Aloha.MessageStreams.Kafka.Clients;
using Aloha.MessageStreams.Kafka.Publishers;
using DryIoc;

namespace Aloha.MessageStreams.Kafka
{
    public static class Extensions
    {
        private const string SectionName = "kafka";

        public static IAlohaBuilder AddKafka(this IAlohaBuilder builder, string sectionName = SectionName)
        {
            builder.Container.Register<IKafkaClient, KafkaClient>();

            builder.Container.Register<IStreamPublisher, KafkaPublisher>();

            return builder;
        }
    }
}
