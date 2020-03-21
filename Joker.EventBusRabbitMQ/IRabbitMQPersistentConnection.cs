using RabbitMQ.Client;
using System;

namespace Joker.EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection
          : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
