

using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Contracts;
using NHibernate;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository
{
    public interface IUnitOfWork
    {
        #region Transaction Control
        void BeginTransaction();
        void Commit();
        void Rollback();
        #endregion

        #region Members
        ICustomerRepository Customer { get; }
        IProductRepository Product { get; }
        ISkuRepository Sku { get; }
        IOrderRepository Order { get; }
        IOrderItemRepository OrderItem { get; }
        #endregion
    }
}
