

using DellTalkNET_UsandoRabbitMQ.Application.Common.Configurations;
using RabbitMQ.Client;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.MessageBroker
{
    public abstract class MessageBrokerConnectionFactory
    {
        protected readonly RabbitMQConfig _config;

        protected static ConnectionFactory? _factory = null;

        protected MessageBrokerConnectionFactory(RabbitMQConfig rabbitMQConfig)
        {
            this._config = rabbitMQConfig;
            if (_factory == null)
            {
                _factory = new()
                {
                    HostName = _config.Host,
                    Port = (_config.Port ?? 5672),
                    UserName = _config.Username,
                    Password = _config.Password,
                    VirtualHost = _config.VirtualHost
                };
            }
        }
    }
}
