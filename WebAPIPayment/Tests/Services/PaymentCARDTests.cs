using PaymentAPI.Domain;
using PaymentAPI.Domain.Entities;
using PaymentAPI.Services.Interface;

namespace Tests.Services
{
    [UseAutofacTestFramework]
    public class PaymentCARDTests
    {
        private readonly IPaymentCARD _payment;
        public PaymentCARDTests(IPaymentCARD payment)
        {
            _payment = payment;
        }
        [Fact]
        public void TestsPayment_Method_CARD()
        {
            var newDTO = new CardModel()
            {
                NameCard = "Rubens Santos",
                NumberCard = "5360434865677524",
                Expiration = "02/24",
                CVV = "546"
            };

            var res = _payment.InsertPayment(newDTO);

            res = _payment.InsertPayment(newDTO);
            res.Error.Should().Be(false);
            res.Status.Should().Be("PAID");
        }
    }
}