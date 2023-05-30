using PaymentAPI.Domain.Entities;

namespace PaymentAPI.Services.Interface
{
    public interface IPaymentTicket
    {
        ResponseDTO InsertPayment();
    }
}