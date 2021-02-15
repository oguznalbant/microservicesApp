using RabbitMQ.Client;
using System;

namespace ECom.EventBusRabbitMq
{
    public interface IRabbitMQConnection : IDisposable
    {
        IModel CreateModel();

        bool IsConnected { get; }

        bool TryConnect();
    }
}
