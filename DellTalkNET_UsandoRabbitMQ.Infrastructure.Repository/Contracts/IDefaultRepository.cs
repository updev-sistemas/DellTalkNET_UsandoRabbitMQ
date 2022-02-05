
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains;
using System.Linq.Expressions;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Contracts
{
    public interface IDefaultRepository<T> where T : EntityBase
    {
        T Get(long id);
        IQueryable<T> Query();
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression);
        void SaveOrUpdate(T entity);
        void Delete(T entity);
    }
}
