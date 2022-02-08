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
            _skuRepository = new SkuRepository(_session);
            _orderRepository = new OrderRepository(_session);
            _orderItemRepository = new OrderItemRepository(_session);
        }
        #endregion

        #region Private Members 

        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISkuRepository _skuRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        #endregion

        #region Members
        public ICustomerRepository Customer => _customerRepository;
        public IProductRepository Product => _productRepository;
        public ISkuRepository Sku => _skuRepository;
        public IOrderRepository Order => _orderRepository;
        public IOrderItemRepository OrderItem => _orderItemRepository;

        #endregion

        #region Transaction Control

        private readonly ISession _session;

        private ITransaction? _transaction;

        public void BeginTransaction()
        {
            if (!_session.IsConnected)
            {
                _session.Reconnect();
            }

            if (_transaction != null)
            {
                throw new Exception("Transaction in progress.");
            }

            _session.Flush();
            _transaction =  _session.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
            _session.Flush();
            _transaction = null;
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _session.Flush();
            _transaction = null;
        }

        public void Dispose()
        {
            _session?.Flush();
            _session?.Close();
        }
        #endregion
    }
}
