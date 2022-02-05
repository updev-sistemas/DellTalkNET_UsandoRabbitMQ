

using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Contracts;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository
{
    public interface IUnitOfWork
    {
        #region Transaction Control
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        #endregion

        #region Members
        ICustomerRepository Customer { get; }
        IProductRepository Product { get; }
        #endregion
    }
}
