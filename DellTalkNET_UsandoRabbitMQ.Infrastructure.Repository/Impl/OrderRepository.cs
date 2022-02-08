using DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Contracts;
using NHibernate;
using System.Linq.Expressions;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Impl
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ISession _db;

        public OrderRepository(ISession session)
            => _db = session;

        public void Delete(Order entity)
            => _db.Delete(entity);

        public Order Get(long id)
            => _db.Get<Order>(id);

        public IEnumerable<Order> GetAll()
            => Query().ToArray();

        public IEnumerable<Order> GetAll(Expression<Func<Order, bool>> expression)
            => Query().Where(expression).ToArray();

        public IQueryable<Order> Query()
            => _db.Query<Order>();

        public void SaveOrUpdate(Order entity)
        {
            if (!entity.CreatedAt.HasValue)
                entity.CreatedAt = DateTime.Now;

            entity.UpdatedAt = DateTime.Now;

            _db.SaveOrUpdate(entity);
        }
    }
}
