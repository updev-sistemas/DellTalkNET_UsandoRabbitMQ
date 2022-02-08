using DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Contracts;
using NHibernate;
using System.Linq.Expressions;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Impl
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        private readonly ISession _db;

        public InvoiceItemRepository(ISession session)
            => _db = session;

        public void Delete(InvoiceItem entity)
            => _db.Delete(entity);

        public InvoiceItem Get(long id)
            => _db.Get<InvoiceItem>(id);

        public IEnumerable<InvoiceItem> GetAll()
            => Query().ToArray();

        public IEnumerable<InvoiceItem> GetAll(Expression<Func<InvoiceItem, bool>> expression)
            => Query().Where(expression).ToArray();

        public IQueryable<InvoiceItem> Query()
            => _db.Query<InvoiceItem>();

        public void SaveOrUpdate(InvoiceItem entity)
        {
            if (!entity.CreatedAt.HasValue)
                entity.CreatedAt = DateTime.Now;

            entity.UpdatedAt = DateTime.Now;

            _db.SaveOrUpdate(entity);
        }
    }
}
