using Aloha.Streams.AmazonKinesis.Clients;
using DryIoc;

namespace Aloha.Streams.AmazonKinesis
{
    public static class Extensions
    {
        private const string SectionName = "amazonKinesis";

        public static IAlohaBuilder AddAmazonKinesis(this IAlohaBuilder builder, string sectionName = SectionName)
        {
            var options = builder.GetOptions<AmazonKinesisOptions>(sectionName);

            builder.Container.RegisterInstance(options);

            builder.Container.Register<IAmazonKinesisClient, AmazonKinesisClient>();

            return builder;
        }
    }
}
