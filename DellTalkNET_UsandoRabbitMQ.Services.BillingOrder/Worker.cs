using DellTalkNET_UsandoRabbitMQ.Application.Common.Configurations;
using DellTalkNET_UsandoRabbitMQ.Application.Common.Const;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.MessageBroker.Contracts;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository;
using DellTalkNET_UsandoRabbitMQ.Services.BillingOrder.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;


namespace DellTalkNET_UsandoRabbitMQ.Services.BillingOrder
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IUnitOfWork _db;
        private readonly IOptions<RabbitMQConfig> _rabbitMQConfig;
        private readonly IMessageBrokerPublish _publish;
        private IConnection _connection;
        private IModel _channel;


        public Worker(ILogger<Worker> logger, IUnitOfWork unitOfWork, IOptions<RabbitMQConfig> rabbitMQConfig, IMessageBrokerPublish publish)
        {
            this._logger = logger;
            this._db = unitOfWork;
            this._rabbitMQConfig = rabbitMQConfig;
            this._publish = publish;
            InitRabbitMQ();
        }


        private void InitRabbitMQ()
        {
            ConnectionFactory _factory = new()
            {
                HostName = _rabbitMQConfig.Value.Host,
                Port = (_rabbitMQConfig.Value.Port ?? 5672),
                UserName = _rabbitMQConfig.Value.Username,
                Password = _rabbitMQConfig.Value.Password,
                VirtualHost = _rabbitMQConfig.Value.VirtualHost
            };

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare($"{QueueConst.QUEUE_BILLING_ORDER}.exchange", ExchangeType.Topic);
            _channel.QueueBind(QueueConst.QUEUE_BILLING_ORDER, $"{QueueConst.QUEUE_BILLING_ORDER}.exchange", $"{QueueConst.QUEUE_BILLING_ORDER}.queue.*", null);
            _channel.BasicQos(0, 1, false);

            _channel.QueueDeclare(queue: QueueConst.QUEUE_BILLING_ORDER, durable: true, exclusive: false, autoDelete: false, arguments: null);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            EventingBasicConsumer? consumer = new(_channel);
            consumer.Received += (ch, ea) =>
            {
                // received message  
                string content = Encoding.UTF8.GetString(ea.Body.ToArray());

                // handle the received message  
                HandleMessage(content);
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(QueueConst.QUEUE_BILLING_ORDER, false, consumer);
            return Task.CompletedTask;
        }

        private void HandleMessage(string content)
        {
            _db.BeginTransaction();
            try
            {
                OrderToBillingModel orderToQueue = JsonSerializer.Deserialize<OrderToBillingModel>(content);

                Infrastructure.Domains.Order? order = _db.Order.Query().Where(x => x.Id == orderToQueue.OrderId && !x.AuthorizeDate.HasValue).FirstOrDefault();

                if (order == null)
                    throw new Exception($"Pedido {orderToQueue.OrderId} não foi localizado ou já foi processado.");

                order.AuthorizeDate = DateTime.Now;

                _db.Order.SaveOrUpdate(order);
                _db.Commit();

                _publish.ToQueue(QueueConst.QUEUE_TAX_ORDER, content);
            }
            catch (Exception ex)
            {
                _db.Rollback();
                _logger.LogError(ex.Message);
            }
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}