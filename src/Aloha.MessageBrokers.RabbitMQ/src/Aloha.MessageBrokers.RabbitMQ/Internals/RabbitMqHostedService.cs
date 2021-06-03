using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers.RabbitMQ.Internals
{
    internal sealed class RabbitMqHostedService : IHostedService
    {
        private readonly IConnection _connection;

        public RabbitMqHostedService(IConnection connection)
        {
            _connection = connection;
        }

        public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _connection.Close();

            return Task.CompletedTask;
        }
    }
}
