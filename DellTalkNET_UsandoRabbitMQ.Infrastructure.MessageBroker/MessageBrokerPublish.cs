
using DellTalkNET_UsandoRabbitMQ.Application.Common.Configurations;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.MessageBroker.Contracts;
using Microsoft.Extensions.Options;
using System.Text;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.MessageBroker
{
    public class MessageBrokerPublish : MessageBrokerConnectionFactory, IMessageBrokerPublish
    {
        public MessageBrokerPublish(IOptions<RabbitMQConfig> rabbitMQConfig)
            :base(rabbitMQConfig.Value)
        {
        }

        public void ToQueue(string queueName, string payload)
        {
            using RabbitMQ.Client.IConnection? connection = _factory?.CreateConnection();
            using RabbitMQ.Client.IModel? channel = connection?.CreateModel();

            channel?.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            byte[]? body = Encoding.UTF8.GetBytes(payload);
            
            channel?.BasicPublish(exchange: "", routingKey: queueName, mandatory:false, basicProperties: null, body: body);
        }
    }
}
