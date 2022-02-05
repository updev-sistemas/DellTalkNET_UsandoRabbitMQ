using DellTalkNET_UsandoRabbitMQ.Application.Common.Configurations;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Database.Contracts;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Database.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Options;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Database.Impl
{
    public sealed class DatabaseFactory : IDatabaseFactory
    {
        private readonly DatabaseConfig _config;

        private static ISessionFactory? _factory = null;

        public DatabaseFactory(IOptions<DatabaseConfig> config)
            => _config = config.Value;  

        private ISessionFactory Initialize()
        {
            if (_factory == null)
            {
                _factory = Fluently.Configure()
                                   .Database(MySQLConfiguration.Standard.ConnectionString(c => c.Database(_config.DbName)
                                                                                                 .Username(_config.Username)
                                                                                                 .Password(_config.Password)
                                                                                                 .Server(_config.Host)
                                                                                                 .Port(_config.Port ?? 3304)))
                                   .Mappings(m =>
                                   {
                                       m.FluentMappings.AddFromAssemblyOf<CustomerMap>()
                                                       .AddFromAssemblyOf<InvoiceMap>()
                                                       .AddFromAssemblyOf<InvoiceItemMap>()
                                                       .AddFromAssemblyOf<OrderMap>()
                                                       .AddFromAssemblyOf<OrderItemMap>()
                                                       .AddFromAssemblyOf<ProductMap>()
                                                       .AddFromAssemblyOf<SkuMap>();
                                   })
                                   .ExposeConfiguration(cfg =>
                                   {
                                       cfg.SetProperty("adonet.batch_size", "100");
                                   })
                                   .BuildSessionFactory();
            }

            return _factory;
        }

        public ISessionFactory GetFactory 
            => Initialize();
    }
}
