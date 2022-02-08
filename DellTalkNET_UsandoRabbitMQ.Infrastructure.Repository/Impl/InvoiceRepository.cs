using DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Contracts;
using NHibernate;
using System.Linq.Expressions;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Impl
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ISession _db;

        public InvoiceRepository(ISession session)
            => _db = session;

        public void Delete(Invoice entity)
            => _db.Delete(entity);

        public Invoice Get(long id)
            => _db.Get<Invoice>(id);

        public IEnumerable<Invoice> GetAll()
            => Query().ToArray();

        public IEnumerable<Invoice> GetAll(Expression<Func<Invoice, bool>> expression)
            => Query().Where(expression).ToArray();

        public IQueryable<Invoice> Query()
            => _db.Query<Invoice>();

        public void SaveOrUpdate(Invoice entity)
        {
            if (!entity.CreatedAt.HasValue)
                entity.CreatedAt = DateTime.Now;

            entity.UpdatedAt = DateTime.Now;

            _db.SaveOrUpdate(entity);
        }
    }
}
