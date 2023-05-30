using PaymentAPI.Domain.Entities;
using PaymentAPI.Services.Interface;

namespace PaymentAPI.Services
{
    public class PaymentPIX : IPaymentPIX
    {
        public PaymentPIX() { }
        public ResponseDTO InsertPayment(OrderCreateDTO dto)
        {
            var res = new ResponseDTO();
            
            res.Error = false;
            res.Status = "PAID";

            return res;
        }

    }
}
