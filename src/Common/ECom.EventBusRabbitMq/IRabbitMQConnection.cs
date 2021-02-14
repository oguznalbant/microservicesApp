using RabbitMQ.Client;

namespace ECom.EventBusRabbitMq
{
    public interface IRabbitMQConnection
    {
        IModel CreateModel();

        bool IsConnected { get; set; }

        bool TryConnect();

        void Dispose();
    }
}
