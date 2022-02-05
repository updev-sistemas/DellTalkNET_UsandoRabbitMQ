using DellTalkNET_UsandoRabbitMQ.Application.Common.Configurations;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.MessageBroker.Contracts
{
    public class MessageBrokerConsumer : MessageBrokerConnectionFactory, IMessageBrokerConsumer
    {
        private IConnection? connection;
        private IModel? channel;

        public MessageBrokerConsumer(IOptions<RabbitMQConfig> rabbitMQConfig)
            : base(rabbitMQConfig.Value)
        {
            if(connection == null)
            {
                connection = _factory?.CreateConnection();
            }

            if (channel == null)
            {
                channel = connection?.CreateModel();
            }
        }

        public void Read(string queueName, Action<string> consumerReceived)
        {
            channel?.ExchangeDeclare($"{queueName}.exchange", ExchangeType.Topic);
            channel?.QueueDeclare($"{queueName}.queue.log", false, false, false, null);
            channel?.QueueBind($"{queueName}.queue.log", $"{queueName}.exchange", $"{queueName}.queue.*", null);
            channel?.BasicQos(0, 1, false);

            channel?.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                // received message  
                string content = Encoding.UTF8.GetString(ea.Body.ToArray());

                // handle the received message  
                consumerReceived(content);
                channel?.BasicAck(ea.DeliveryTag, false);
            }; ;

            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
