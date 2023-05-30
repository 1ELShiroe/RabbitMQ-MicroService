using PaymentAPI.Domain.Entities;

namespace PaymentAPI.Services.Interface
{
    public interface IPaymentPIX
    {
        ResponseDTO InsertPayment(OrderCreateDTO dto);
    }
}