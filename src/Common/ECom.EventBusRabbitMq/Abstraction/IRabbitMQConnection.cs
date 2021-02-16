using RabbitMQ.Client;
using System;

namespace ECom.EventBusRabbitMq.Abstraction
{
    public interface IRabbitMQConnection : IDisposable
    {
        IModel CreateModel();

        bool IsConnected { get; }

        bool TryConnect();
    }
}
