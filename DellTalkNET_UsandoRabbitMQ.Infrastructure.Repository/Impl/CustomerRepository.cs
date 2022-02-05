using DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Contracts;
using NHibernate;
using System.Linq.Expressions;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Impl
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ISession _db;

        public CustomerRepository(ISession session)
            => _db = session;

        public void Delete(Customer entity)
            => _db.Delete(entity);

        public Customer Get(long id) 
            => _db.Get<Customer>(id);

        public IEnumerable<Customer> GetAll() 
            => Query().ToArray();

        public IEnumerable<Customer> GetAll(Expression<Func<Customer, bool>> expression) 
            => Query().Where(expression).ToArray();

        public IQueryable<Customer> Query()
            => _db.Query<Customer>();

        public void SaveOrUpdate(Customer entity)
        {
            if (!entity.CreatedAt.HasValue)
                entity.CreatedAt = DateTime.Now;

            entity.UpdatedAt = DateTime.Now;

            _db.SaveOrUpdate(entity);
        }
    }
}
