using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace SocialMiner.Core.Infrastructure.Caching
{
    public class RedisClient
          
    {
        public RedisClient(string connectionString)
        {
            ConfigurationOptions option = new ConfigurationOptions
            {
                AbortOnConnectFail = false
            };

            option.EndPoints.Add(connectionString);

            _connection = ConnectionMultiplexer.Connect(option);
            _database = _connection.GetDatabase();
        }

        private readonly IDatabase _database;
        private readonly ConnectionMultiplexer _connection;

        public JsonSerializerSettings Settings { get; set; } = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto
        };

        public async Task<T> GetAsync<T>(string key)
        {
            string value = await GetAsync(key);

            if (string.IsNullOrEmpty(value))
                return default(T);

            return JsonConvert.DeserializeObject<T>(value, Settings);
        }

        public async Task<string> GetAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expirationTime = null)
        {
            string objectValue = JsonConvert.SerializeObject(value, Formatting.None, Settings);
            return await SetAsync(key, objectValue, expirationTime);
        }

        public async Task<bool> SetAsync(string key, string value, TimeSpan? expirationTime = null)
        {
            return await _database.StringSetAsync(key, value, expirationTime);
        }

        public async Task Remove(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed)
                return;

            _connection.Dispose();
            _disposed = true;
        }

        #endregion
    }
}
