using PaymentAPI.infrastructure.Interfaces;
using PaymentAPI.Repositories.interfaces;
using PaymentAPI.Domain.Entities;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace PaymentAPI.infrastructure.MessageQueue
{
    public class Consumer : BackgroundService, IConsumer
    {
        private readonly IPaymentRepository _payment;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string QueueMQ = "order-payment";
        private const string TrackingsExchange = "tracking-service";
        private const string RouterSubScribe = "order-create";

        public Consumer(IPaymentRepository payment)
        {
            _payment = payment;
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
                var @event = JsonConvert.DeserializeObject<OrderCreateDTO>(contentString);

                Console.WriteLine($"new payment made successfully!");

                Listening(@event!).Wait();

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(QueueMQ, false, consumer);

            return Task.CompletedTask;
        }

        public Task Listening(OrderCreateDTO @event)
        {
            System.Console.WriteLine($"New payment requested for order with ID: {@event.OrderId}");
            ResponseDTO data = _payment.MakePayment(@event);

            if (data.Error)
            {
                Console.WriteLine("ERROR NA APLICAÇÂO");
                return Task.CompletedTask;
            }
            
            return Task.CompletedTask;
        }
    }
}