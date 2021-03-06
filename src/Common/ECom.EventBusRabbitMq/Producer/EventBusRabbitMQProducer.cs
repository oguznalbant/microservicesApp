﻿using ECom.EventBusRabbitMq.Abstraction;
using ECom.EventBusRabbitMq.Events;
using ECom.EventBusRabbitMq.Producer.Abstract;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace ECom.EventBusRabbitMq.Producer
{
    public class EventBusProducer : IEventBusProducer<ShoppingCartCheckoutEvent>
    {
        private readonly IRabbitMQConnection _rabbitMQConnection;

        public EventBusProducer(IRabbitMQConnection rabbitMQConnection)
        {
            _rabbitMQConnection = rabbitMQConnection;
        }

        public void Publish(string queueName, ShoppingCartCheckoutEvent publishModel)
        {
            using (var channel = _rabbitMQConnection.CreateModel()) //begin disposable channel
            {
                channel.QueueDeclare(queueName, false, false, false, null); //queue declared with name

                //serialized and cart converted to the byte 
                var message = JsonConvert.SerializeObject(publishModel);
                var body = Encoding.UTF8.GetBytes(message);

                // defining some traditional properties for publishing queue
                IBasicProperties basicProperties = channel.CreateBasicProperties();
                basicProperties.Persistent = true;
                basicProperties.DeliveryMode = 2;

                channel.ConfirmSelect();
                channel.BasicPublish("", queueName, true, basicProperties, body);
                channel.WaitForConfirmsOrDie();

                channel.BasicAcks += (sender, eventArgs) =>
                {
                    Console.WriteLine("Sent RabbitMQ");
                    //implement ack handle
                };
                channel.ConfirmSelect();
            }
        }
    }
}
