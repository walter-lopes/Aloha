using Aloha.MessageStreams.AmazonKinesis.Clients;
using Aloha.MessageStreams.AmazonKinesis.Publishers;
using Microsoft.Extensions.DependencyInjection;

namespace Aloha.MessageStreams.AmazonKinesis
{
    public static class Extensions
    {
        private const string SectionName = "amazonKinesis";

        public static IAlohaBuilder AddAmazonKinesis(this IAlohaBuilder builder, string sectionName = SectionName)
        {
            var options = builder.GetOptions<AmazonKinesisOptions>(sectionName);

            builder.Services.AddSingleton(options);

            builder.Services.AddSingleton<IAmazonKinesisClient, AmazonKinesisClient>();

            builder.Services.AddSingleton<IStreamPublisher, AmazonKinesisPublisher>();

            return builder;
        }
    }
}
