using DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Contracts;
using NHibernate;
using System.Linq.Expressions;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Impl
{
    public class ProductRepository : IProductRepository
    {
        private readonly ISession _db;

        public ProductRepository(ISession session)
            => _db = session;

        public void Delete(Product entity)
            => _db.Delete(entity);

        public Product Get(long id)
            => _db.Get<Product>(id);

        public IEnumerable<Product> GetAll()
            => Query().ToArray();

        public IEnumerable<Product> GetAll(Expression<Func<Product, bool>> expression)
            => Query().Where(expression).ToArray();

        public IQueryable<Product> Query()
            => _db.Query<Product>();

        public void SaveOrUpdate(Product entity)
        { 
            if(!entity.CreatedAt.HasValue)
                entity.CreatedAt = DateTime.Now;    

            entity.UpdatedAt = DateTime.Now;    

            _db.SaveOrUpdate(entity);
        }
    }
}
