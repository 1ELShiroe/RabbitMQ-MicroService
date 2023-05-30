using API.Domain.DTOs;

namespace API.Repository.interfaces
{
    public interface IPaymentRepository
    {
        String InsertPayment(OrderPaymentDTO dto);
    }
}