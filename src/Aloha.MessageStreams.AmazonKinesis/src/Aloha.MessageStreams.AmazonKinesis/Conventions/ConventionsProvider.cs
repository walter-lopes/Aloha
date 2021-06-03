using System;
using System.Collections.Concurrent;

namespace Aloha.MessageStreams.AmazonKinesis.Conventions
{
    public class ConventionsProvider : IConventionsProvider
    {
        private readonly ConcurrentDictionary<Type, IConventions> _conventions =
        new ConcurrentDictionary<Type, IConventions>();

        private readonly AmazonKinesisOptions _options;

        public ConventionsProvider(AmazonKinesisOptions options)
        {
            _options = options;
        }

        public IConventions Get<T>() => Get(typeof(T));

        public IConventions Get(Type type)
        {
            if (_conventions.TryGetValue(type, out var conventions))
            {
                return conventions;
            }

            conventions = new MessageConventions(type, _options.Streams[type.Name]);

            _conventions.TryAdd(type, conventions);

            return conventions;
        }
    }
}
