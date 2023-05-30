using PaymentAPI.Domain;
using PaymentAPI.Domain.Entities;
using PaymentAPI.Repositories.interfaces;

namespace Tests.Repositories
{
    [UseAutofacTestFramework]
    public class PaymentRepositoryTests
    {
        private readonly IPaymentRepository _repository;
        public PaymentRepositoryTests(IPaymentRepository repository)
        {
            _repository = repository;
        }

        [Fact]
        public void TestPaymentRepository_MakePayment()
        {
            var newDTO = new OrderCreateDTO()
            {
                OrderId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Amount = (decimal)10.5,
                Method = "CARD"
            };

            var res = _repository.MakePayment(newDTO);

            res.Error.Should().Be(true);
            res.Message.Should().Be("no card found within the request");

            newDTO.Card = new CardModel()
            {
                NameCard = "Rubens Santos",
                NumberCard = "5360434865677524",
                Expiration = "02/24",
                CVV = "546"
            };

            res = _repository.MakePayment(newDTO);
            res.Error.Should().Be(false);
            res.Status.Should().Be("PAID");

            newDTO.Method = "PIX";
            res = _repository.MakePayment(newDTO);
            res.Error.Should().Be(false);
            res.Status.Should().Be("PAID");

            newDTO.Method = "TICKET";
            res = _repository.MakePayment(newDTO);
            res.Error.Should().Be(false);
            res.Status.Should().Be("IN PROCESS");
        }
    }
}