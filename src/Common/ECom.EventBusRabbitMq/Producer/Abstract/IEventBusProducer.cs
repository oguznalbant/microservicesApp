using ECom.EventBusRabbitMq.Abstraction;

namespace ECom.EventBusRabbitMq.Producer.Abstract
{
    public interface IEventBusProducer<TModel> where TModel : IPublishEvent
    {
        void Publish(string queueName, TModel publishModel);
    }
}
