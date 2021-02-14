using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ECom.EventBusRabbitMq
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private bool _disposed;

        public RabbitMQConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;

            if (!IsConnected)
            {
                TryConnect();
            }
        }

        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;
            }
            set { };
        }

        public IModel CreateModel()
        {

            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connection!");
            }

            return _connection.CreateModel(); //creating new channel for quequ 
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            try
            {
                _connection.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool TryConnect()
        {
            try
            {
                _connection = _connectionFactory.CreateConnection();
            }
            catch (BrokerUnreachableException)
            {
                Thread.Sleep(2000); // waiting 2 seconds also trying again connection
                _connection = _connectionFactory.CreateConnection();
            }

            //again check for connection
            if (IsConnected)
            {
                return true;
            }

            return false;
        }
    }
}
