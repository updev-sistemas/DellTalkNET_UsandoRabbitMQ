using DellTalkNET_UsandoRabbitMQ.Application.Common.Configurations;
using DellTalkNET_UsandoRabbitMQ.Application.Common.Const;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.MessageBroker.Contracts;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository;
using DellTalkNET_UsandoRabbitMQ.Services.ProcessingOrder.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace DellTalkNET_UsandoRabbitMQ.Services.ProcessingOrder
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

            _channel.ExchangeDeclare($"{QueueConst.QUEUE_NEW_ORDER}.exchange", ExchangeType.Topic);
            _channel.QueueBind(QueueConst.QUEUE_NEW_ORDER, $"{QueueConst.QUEUE_NEW_ORDER}.exchange", $"{QueueConst.QUEUE_NEW_ORDER}.queue.*", null);
            _channel.BasicQos(0, 1, false);

            _channel.QueueDeclare(queue: QueueConst.QUEUE_NEW_ORDER, durable: true, exclusive: false, autoDelete: false, arguments: null);

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

            _channel.BasicConsume(QueueConst.QUEUE_NEW_ORDER, false, consumer);
            return Task.CompletedTask;
        }

        private void HandleMessage(string content)
        {
            _db.BeginTransaction();
            try
            {
                OrderCreateModel orderToQueue = JsonSerializer.Deserialize<OrderCreateModel>(content);

                Infrastructure.Domains.Customer? customer = _db.Customer.GetAll(x => x.Document == orderToQueue.Customer.Document).FirstOrDefault();
                if (customer == null)
                {
                    customer = new Infrastructure.Domains.Customer
                    {
                        Birthday = orderToQueue.Customer.Birthday,
                        Document = orderToQueue.Customer.Document,
                        Email = orderToQueue.Customer.Email,
                        Name = orderToQueue.Customer.Name
                    };
                }

                _db.Customer.SaveOrUpdate(customer);

                Infrastructure.Domains.Order order = new()
                {
                    Customer = customer,
                    AuthorizeDate = null,
                    Date = orderToQueue.Date,
                    Number = DateTime.Now.ToString("yyyyMMddhhmmss")
                };

                _db.Order.SaveOrUpdate(order);

                for (int i = 0; i < orderToQueue.Items.Count; i++)
                {
                    Infrastructure.Domains.Product? product = _db.Product.Query().Where(x => x.Code == orderToQueue.Items[i].Product.Code).FirstOrDefault();

                    if (product == null)
                        throw new Exception($"Produto {orderToQueue.Items[i].Product.Code} não foi localizado.");

                    Infrastructure.Domains.OrderItem item = new()
                    {
                        Order = order,
                        Amount = orderToQueue.Items[i].Amount,
                        Cost = orderToQueue.Items[i].Cost,
                        Sequence = Convert.ToInt16(1 + i),
                        Product = product
                    };

                    _db.OrderItem.SaveOrUpdate(item);
                }

                _db.Commit();

                string payload = JsonSerializer.Serialize(new
                {
                    OrderId = order.Id.Value
                });
                _publish.ToQueue(QueueConst.QUEUE_BILLING_ORDER, payload);
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