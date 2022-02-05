using RabbitMQ.Client.Events;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.MessageBroker.Contracts
{
    public interface IMessageBrokerConsumer
    {
        void Read(string queueName, Action<string> consumerReceived);
    }
}
