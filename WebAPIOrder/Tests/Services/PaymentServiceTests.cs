using API.Infrastructure.Interface;
using API.services.interfaces;
using API.Domain.Models;
using API.Domain.DTOs;

namespace Tests.Services
{
    [UseAutofacTestFramework]
    public class PaymentServiceTests
    {
        private readonly IPublisher _rabbitMQHandler;

        private readonly IPaymentService _paymentService;
        public PaymentServiceTests(IPublisher rabbitMQHandler, IPaymentService paymentService)
        {
            _rabbitMQHandler = rabbitMQHandler;
            _paymentService = paymentService;
        }
        [Fact]
        public void PaymentService_GenerateMethod_Tests()
        {
            var paymentDTO = new OrderPaymentDTO()
            {
                OrderId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Amount = (Decimal)20.5,
                Method = "card",
            };

            var methodDTO = _paymentService.GenerateMethod(paymentDTO);
            methodDTO.Should().NotBeNull();

            methodDTO.Error.Should().Be(true);
            methodDTO.Message.Should().Be("invalid card information");

            paymentDTO.Card = new CardModel()
            {
                CVV = "655",
                NameCard = "Ricardo S Santos",
                NumberCard = "5184092978435521",
                Expiration = "01/25"
            };

            methodDTO = _paymentService.GenerateMethod(paymentDTO);
            methodDTO.Should().NotBeNull();

            methodDTO.Error.Should().Be(false);
            methodDTO.Message.Should().Be("payment request sent successfully");
        }
    }
}