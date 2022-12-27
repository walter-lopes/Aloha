using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Aloha.CQRS.Events.Repositories
{
    public class MongoContext : IDbEventsContext
    {
        public string ConnectionString { get; set; }
        public string DataBase { get; set; }

        const string REGISTER_IGNORE_CONVENTION = "IgnoreConvention";
        const string REGISTER_ENUM_CONVENTION = "EnumConvention";

        public IMongoDatabase Context
        {
            get
            {
                MongoUrl url = new(this.ConnectionString);


                MongoClient client = new MongoClient(url);

                ConventionRegistry.Register(REGISTER_IGNORE_CONVENTION, new ConventionPack
                {
                    new IgnoreIfDefaultConvention(true),
                    new IgnoreExtraElementsConvention(true)
                }, t => true);

                ConventionRegistry.Register(REGISTER_ENUM_CONVENTION,
                    new ConventionPack {new EnumRepresentationConvention(BsonType.String)}, t => true);

                return client.GetDatabase(this.DataBase,
                    new MongoDatabaseSettings {GuidRepresentation = GuidRepresentation.Standard});
            }
        }
    }
}