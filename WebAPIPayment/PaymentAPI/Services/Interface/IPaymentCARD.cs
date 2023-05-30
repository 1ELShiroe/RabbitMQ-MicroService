using PaymentAPI.Domain;
using PaymentAPI.Domain.Entities;

namespace PaymentAPI.Services.Interface
{
    public interface IPaymentCARD
    {
        ResponseDTO InsertPayment(CardModel card);
    }
}