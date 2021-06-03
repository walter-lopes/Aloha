using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Aloha.Serializers
{
    public class NewtonsoftJsonAlohaSerializer : IAlohaSerializer
    {
        private readonly JsonSerializerSettings _settings;

        public NewtonsoftJsonAlohaSerializer(JsonSerializerSettings settings = null)
        {
            _settings = settings ?? new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public string Serialize<T>(T value) => JsonConvert.SerializeObject(value, _settings);

        public string Serialize(object value) => JsonConvert.SerializeObject(value, _settings);

        public T Deserialize<T>(string value) => JsonConvert.DeserializeObject<T>(value, _settings);

        public object Deserialize(string value) => JsonConvert.DeserializeObject(value, _settings);
    }
}
