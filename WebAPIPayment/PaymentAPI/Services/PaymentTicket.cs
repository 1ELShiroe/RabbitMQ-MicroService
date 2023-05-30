using PaymentAPI.Domain.Entities;
using PaymentAPI.Services.Interface;

namespace PaymentAPI.Services
{
    public class PaymentTicket : IPaymentTicket
    {
        public PaymentTicket() { }

        public ResponseDTO InsertPayment()
        {
            var res = new ResponseDTO();

            res.Error = false;
            res.Status = "IN PROCESS";

            return res;
        }

    }
}