using NHibernate;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Database.Contracts
{
    public interface IDatabaseFactory
    {
        ISessionFactory GetFactory { get; }
    }
}
