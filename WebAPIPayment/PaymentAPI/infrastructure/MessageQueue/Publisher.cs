using PaymentAPI.infrastructure.Interfaces;
using PaymentAPI.Domain.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace PaymentAPI.infrastructure.MessageQueue
{
    public class Publisher : IPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string TrackingsExchange = "tracking-service";

        public Publisher()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "123456789"
            };

            _connection = connectionFactory.CreateConnection("ELRabbit");
            _channel = _connection.CreateModel();
        }

        public void Publish(OrderCreateDTO dto, String queue)
        {
            var payLoad = JsonConvert.SerializeObject(dto);
            var byteArray = Encoding.UTF8.GetBytes(payLoad);

            _channel.BasicPublish(TrackingsExchange, queue, null, byteArray);
        }
    }
}