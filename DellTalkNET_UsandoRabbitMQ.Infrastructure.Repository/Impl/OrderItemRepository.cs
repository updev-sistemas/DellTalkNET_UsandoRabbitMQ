using DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Contracts;
using NHibernate;
using System.Linq.Expressions;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Impl
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ISession _db;

        public OrderItemRepository(ISession session)
            => _db = session;

        public void Delete(OrderItem entity)
            => _db.Delete(entity);

        public OrderItem Get(long id)
            => _db.Get<OrderItem>(id);

        public IEnumerable<OrderItem> GetAll()
            => Query().ToArray();

        public IEnumerable<OrderItem> GetAll(Expression<Func<OrderItem, bool>> expression)
            => Query().Where(expression).ToArray();

        public IQueryable<OrderItem> Query()
            => _db.Query<OrderItem>();

        public void SaveOrUpdate(OrderItem entity)
        {
            if (!entity.CreatedAt.HasValue)
                entity.CreatedAt = DateTime.Now;

            entity.UpdatedAt = DateTime.Now;

            _db.SaveOrUpdate(entity);
        }
    }
}
