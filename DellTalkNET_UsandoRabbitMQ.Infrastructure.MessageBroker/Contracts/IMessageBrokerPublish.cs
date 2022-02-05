namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.MessageBroker.Contracts
{
    public interface IMessageBrokerPublish
    {
        void ToQueue(string queueName, string payload);
    }
}
