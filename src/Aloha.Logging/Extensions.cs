using Aloha.Logging.Factories;
using Aloha.Logging.Loggers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            builder.Services.AddSingleton(logger);

            var logEvent = new LogEvent(Guid.NewGuid(), Guid.NewGuid());

            builder.Services.Add(new ServiceDescriptor(typeof(ILogger), new SerilogLogger(logger, logEvent)));

            return builder;
        }
    }
}
