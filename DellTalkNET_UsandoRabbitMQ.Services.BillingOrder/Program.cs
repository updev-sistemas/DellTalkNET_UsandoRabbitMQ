using DellTalkNET_UsandoRabbitMQ.Application.Common.Configurations;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Database.Contracts;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Database.Impl;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.MessageBroker;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.MessageBroker.Contracts;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository;
using DellTalkNET_UsandoRabbitMQ.Services.BillingOrder;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddOptions();

        services.Configure<DatabaseConfig>(hostContext.Configuration.GetSection("Database"));
        services.Configure<RabbitMQConfig>(hostContext.Configuration.GetSection("RabbitMQ"));

        services.AddSingleton<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IDatabaseFactory, DatabaseFactory>();

        services.AddSingleton<IMessageBrokerConsumer, MessageBrokerConsumer>();
        services.AddSingleton<IMessageBrokerPublish, MessageBrokerPublish>();

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
