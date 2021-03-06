using DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Contracts;
using NHibernate;
using System.Linq.Expressions;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Impl
{
    public class SkuRepository : ISkuRepository
    {
        private readonly ISession _db;

        public SkuRepository(ISession session)
            => _db = session;

        public void Delete(Sku entity)
            => _db.Delete(entity);

        public Sku Get(long id)
            => _db.Get<Sku>(id);

        public IEnumerable<Sku> GetAll()
            => Query().ToArray();

        public IEnumerable<Sku> GetAll(Expression<Func<Sku, bool>> expression)
            => Query().Where(expression).ToArray();

        public IQueryable<Sku> Query()
            => _db.Query<Sku>();

        public void SaveOrUpdate(Sku entity)
        {
            if (!entity.CreatedAt.HasValue)
                entity.CreatedAt = DateTime.Now;

            entity.UpdatedAt = DateTime.Now;

            _db.SaveOrUpdate(entity);
        }
    }
}
