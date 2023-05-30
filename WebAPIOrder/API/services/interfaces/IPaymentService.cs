using API.Domain.DTOs;
using API.Domain.Models;

namespace API.services.interfaces
{
    public interface IPaymentService
    {
        ResponseModel GenerateMethod(OrderPaymentDTO dto);
    }
}