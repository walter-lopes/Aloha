namespace Aloha.Persistence.MongoDB.Builders
{
    internal sealed class MongoDbOptionsBuilder : IMongoDbOptionsBuilder
    {
        private readonly MongoDbOptions _options;

        public MongoDbOptionsBuilder()
        {
            _options = new MongoDbOptions();
        }

        public MongoDbOptions Build()
            => _options;

        public IMongoDbOptionsBuilder WithConnectionString(string connectionString)
        {
            _options.ConnectionString = connectionString;
            return this;
        }

        public IMongoDbOptionsBuilder WithDatabase(string database)
        {
            _options.Database = database;
            return this;
        }

        public IMongoDbOptionsBuilder WithSeed(bool seed)
        {
            _options.Seed = seed;
            return this;
        }
    }
}
