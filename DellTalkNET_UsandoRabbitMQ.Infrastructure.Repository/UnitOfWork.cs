using DellTalkNET_UsandoRabbitMQ.Infrastructure.Database.Contracts;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Contracts;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository.Impl;
using NHibernate;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Constructor
        public UnitOfWork(IDatabaseFactory factory)
        {
            _session = factory.GetFactory.OpenSession();

            _session.SetBatchSize(10);
            _session.FlushMode = FlushMode.Auto;

            _customerRepository = new CustomerRepository(_session);
            _productRepository = new ProductRepository(_session);
        }
        #endregion

        #region Private Members 

        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        #endregion

        #region Members
        public ICustomerRepository Customer => _customerRepository;
        public IProductRepository Product => _productRepository;

        #endregion

        #region Transaction Control

        private readonly ISession _session;

        private ITransaction? _transaction;

        public void BeginTransaction()
        {
            if (_transaction != null)
            {
                throw new Exception("Banco está com uma transação em andamento.");
            }

            _transaction = _session.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
        }


        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Rollback();

            _session.Flush();
            _session.Close();
        }
        #endregion
    }
}
