using API.Infrastructure.Interface;
using API.Repository.interfaces;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using API.Domain.DTOs;
using Newtonsoft.Json;
using System.Text;

namespace API.Infrastructure
{
    public class Consumer : BackgroundService, IConsumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string QueueMQ = "order-create";
        private const string TrackingsExchange = "tracking-service";
        private const string RouterSubScribe = "order-payment";
        private readonly IPaymentRepository _paymentRepository;

        public Consumer(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;

            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "123456789"
            };

            _connection = connectionFactory.CreateConnection("ELRabbit");

            _channel = _connection.CreateModel();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _channel.ExchangeDeclare(TrackingsExchange, "topic", true, false);
            _channel.QueueDeclare(QueueMQ, true, false, false);
            _channel.QueueBind(QueueMQ, TrackingsExchange, RouterSubScribe);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var @event = JsonConvert.DeserializeObject<OrderPaymentDTO>(contentString);

                Console.WriteLine($"new payment made successfully");

                Listening(@event!).Wait();

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(QueueMQ, false, consumer);

            return Task.CompletedTask;
        }

        public Task Listening(OrderPaymentDTO @event)
        {
            var msg = _paymentRepository.InsertPayment(@event);
            Console.WriteLine(msg);

            return Task.CompletedTask;
        }

    }
}