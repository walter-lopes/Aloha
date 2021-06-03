using Aloha.Persistence.MongoDB.Builders;
using Aloha.Persistence.MongoDB.Factories;
using Aloha.Persistence.MongoDB.Initializers;
using Aloha.Persistence.MongoDB.Repositories;
using Aloha.Persistence.MongoDB.Seeders;
using Aloha.Types;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;

namespace Aloha.Persistence.MongoDB
{

    public static class Extensions
    {
        // Helpful when dealing with integration testing
        private static bool _conventionsRegistered;
        private const string SectionName = "mongo";
        private const string RegistryName = "persistence.mongoDb";


        const string REGISTER_IGNORE_CONVENTION = "IgnoreConvention";
        const string REGISTER_ENUM_CONVENTION = "EnumConvention";

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

            builder.Services.AddSingleton(mongoOptions);
            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var options = sp.GetService<MongoDbOptions>();
                return new MongoClient(options.ConnectionString);
            });
            builder.Services.AddTransient(sp =>
            {
                var options = sp.GetService<MongoDbOptions>();
                var client = sp.GetService<IMongoClient>();
                return client.GetDatabase(options.Database, new MongoDatabaseSettings { GuidRepresentation = GuidRepresentation.Standard });
            });

            builder.Services.AddTransient<IMongoDbInitializer, MongoDbInitializer>();
            builder.Services.AddTransient<IMongoSessionFactory, MongoSessionFactory>();

            if (seederType is null)
            {
                builder.Services.AddTransient<IMongoDbSeeder, MongoDbSeeder>();
            }
            else
            {
                builder.Services.AddTransient(typeof(IMongoDbSeeder), seederType);
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
            ConventionRegistry.Register(REGISTER_IGNORE_CONVENTION, new ConventionPack
                {
                    new IgnoreIfDefaultConvention(true),
                    new IgnoreExtraElementsConvention(true)
                }, t => true);

            ConventionRegistry.Register(REGISTER_ENUM_CONVENTION, new ConventionPack { new EnumRepresentationConvention(BsonType.String) }, t => true);

        }

        public static IAlohaBuilder AddMongoRepository<TEntity, TIdentifiable>(this IAlohaBuilder builder,
            string collectionName)
            where TEntity : IIdentifiable<TIdentifiable>
        {
            builder.Services.AddTransient<IMongoRepository<TEntity, TIdentifiable>>(sp =>
            {
                var database = sp.GetService<IMongoDatabase>();
                return new MongoRepository<TEntity, TIdentifiable>(database, collectionName);
            });

            return builder;
        }
    }
}
