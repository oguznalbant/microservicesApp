using System;

namespace ECom.EventBusRabbitMq.Abstraction
{
    public interface IPublishEvent
    {
        Guid Id { get; set; }
    }
}