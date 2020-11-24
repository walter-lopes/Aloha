using Aloha.Logging.Factories;
using Aloha.Logging.Loggers;
using DryIoc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace Aloha.Logging
{
    public static class Extensions
    {
        public static IAlohaBuilder UseSerilog(this IAlohaBuilder builder, IConfiguration configuration)
        {
            Serilog.ILogger logger = new LoggerConfiguration()
                                     .ReadFrom.Configuration(configuration)
                                     .CreateLogger(); 

            builder.Container.UseInstance(logger);

            var logEvent = new LogEvent(Guid.NewGuid(), Guid.NewGuid());

            builder.Container.Register<ILogger>(
                reuse: Reuse.Singleton,
                made: Made.Of(() => new SerilogLogger(logger, logEvent)));

            return builder;
        }
    }
}
