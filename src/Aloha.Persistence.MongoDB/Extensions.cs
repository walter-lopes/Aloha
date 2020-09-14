using Aloha.Persistence.MongoDB.Builders;
using Aloha.Persistence.MongoDB.Factories;
using Aloha.Persistence.MongoDB.Initializers;
using Aloha.Persistence.MongoDB.Repositories;
using Aloha.Persistence.MongoDB.Seeders;
using Aloha.Types;
using DryIoc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;

namespace Aloha.Persistence.MongoDB
{
    public static class Extensions
    {
        private static bool _conventionsRegistered;
        private const string SectionName = "mongo";
        private const string RegistryName = "persistence.mongoDb";

        public static IAlohaBuilder AddMongo(this IAlohaBuilder builder, string sectionName = SectionName,
           Type seederType = null, bool registerConventions = true)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            var mongoOptions = builder.GetOptions<MongoDbOptions>(sectionName);
            return builder.AddMongo(mongoOptions, seederType, registerConventions);
        }

        public static IAlohaBuilder AddMongo(this IAlohaBuilder builder, Func<IMongoDbOptionsBuilder,
            IMongoDbOptionsBuilder> buildOptions, Type seederType = null, bool registerConventions = true)
        {
            var mongoOptions = buildOptions(new MongoDbOptionsBuilder()).Build();
            return builder.AddMongo(mongoOptions, seederType, registerConventions);
        }


        public static IAlohaBuilder AddMongo(this IAlohaBuilder builder, MongoDbOptions mongoOptions,
                                    Type seederType = null, bool registerConventions = true)
        {
            if (!builder.TryRegister(RegistryName))
            {
                return builder;
            }

            builder.Container.RegisterInstance(mongoOptions);

            var options = builder.Container.Resolve<MongoDbOptions>();

            builder.Container.Register<IMongoClient>(
                reuse: Reuse.Singleton,
                made:  Made.Of(() => new MongoClient(options.ConnectionString)
            ));

            builder.Container.Register<IMongoDbInitializer, MongoDbInitializer>();
            builder.Container.Register<IMongoSessionFactory, MongoSessionFactory>();

            if (seederType is null)
            {
                builder.Container.Register<IMongoDbSeeder, MongoDbSeeder>();
            }
            else
            {
                builder.Container.Register(typeof(IMongoDbSeeder), seederType);
            }

            builder.AddInitializer<IMongoDbInitializer>();

            if (registerConventions && !_conventionsRegistered)
            {
                RegisterConventions();
            }

            return builder;
        }


        private static void RegisterConventions()
        {
            _conventionsRegistered = true;
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?),
                new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
            ConventionRegistry.Register("aloha", new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
            }, _ => true);
        }

        public static IAlohaBuilder AddMongoRepository<TEntity, TIdentifiable>(this IAlohaBuilder builder,
            string collectionName)
            where TEntity : IIdentifiable<TIdentifiable>
        {
            builder.Container.Register<IMongoRepository<TEntity, TIdentifiable>>(
                reuse: Reuse.Transient,
                made: Made.Of(() => new MongoRepository<TEntity, TIdentifiable>(Arg.Of<IMongoDatabase>(), collectionName)));

            return builder;
        }
    }
}
