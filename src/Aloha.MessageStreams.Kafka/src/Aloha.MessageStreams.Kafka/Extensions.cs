using Aloha.MessageStreams.Kafka.Clients;
using Aloha.MessageStreams.Kafka.Publishers;
using Microsoft.Extensions.DependencyInjection;

namespace Aloha.MessageStreams.Kafka
{
    public static class Extensions
    {
        private const string SectionName = "kafka";

        public static IAlohaBuilder AddKafka(this IAlohaBuilder builder, string sectionName = SectionName)
        {
            builder.Services.AddSingleton<IKafkaClient, KafkaClient>();

            builder.Services.AddSingleton<IStreamPublisher, KafkaPublisher>();

            return builder;
        }
    }
}
