using System;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Joker.EventBusRabbitMQ
{
    public abstract class RabbitMqClientBase : IDisposable
    {
        protected abstract string VirtualHost { get; }
        protected abstract string Exchange { get; }
        protected abstract string Queue { get; }
        protected abstract string QueueAndExchangeRoutingKey { get; }

        protected IModel Channel { get; private set; }
        private IConnection _connection;
        private readonly ConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitMqClientBase> _logger;

        protected RabbitMqClientBase(ConnectionFactory connectionFactory,
            ILogger<RabbitMqClientBase> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
            ConnectToRabbitMq();
        }

        private void ConnectToRabbitMq()
        {
            if (_connection == null || _connection.IsOpen == false)
            {
                _connection = _connectionFactory.CreateConnection();
            }

            if (Channel == null || Channel.IsOpen == false)
            {
                Channel = _connection.CreateModel();
                Channel.ExchangeDeclare(exchange: Exchange, type: "direct", durable: true, autoDelete: false);
                Channel.QueueDeclare(queue: Queue, durable: false, exclusive: false, autoDelete: false);
                Channel.QueueBind(queue: Queue, exchange: Exchange, routingKey: QueueAndExchangeRoutingKey);
            }
        }

        public void Dispose()
        {
            try
            {
                Channel?.Close();
                Channel?.Dispose();
                Channel = null;

                _connection?.Close();
                _connection?.Dispose();
                _connection = null;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Cannot dispose RabbitMQ channel or connection");
            }
        }
    }
}