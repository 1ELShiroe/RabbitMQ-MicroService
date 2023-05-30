using PaymentAPI.Domain.Entities;

namespace PaymentAPI.Repositories.interfaces
{
    public interface IPaymentRepository
    {
        ResponseDTO MakePayment(OrderCreateDTO Method);
    }

}