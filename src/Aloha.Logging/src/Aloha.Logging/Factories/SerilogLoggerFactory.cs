

using Microsoft.Extensions.Logging;
using Serilog.Debugging;
using Serilog.Extensions.Logging;

namespace Aloha.Logging.Factories
{
    public class SerilogLoggerFactory
         : ILoggerFactory
    {
        public SerilogLoggerFactory(Serilog.ILogger logger = null, bool dispose = false)
        {
            _provider = new SerilogLoggerProvider(logger, dispose);
        }

        private readonly SerilogLoggerProvider _provider;

        public void Dispose()
        {
            _provider.Dispose();
        }

        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            return _provider.CreateLogger(categoryName);
        }

        public Microsoft.Extensions.Logging.ILogger CreateLogger<T>()
        {
            return _provider.CreateLogger(typeof(T).Name);
        }

        public void AddProvider(ILoggerProvider provider)
        {
            SelfLog.WriteLine("Ignoring added logger provider {0}", provider);
        }
    }
}
