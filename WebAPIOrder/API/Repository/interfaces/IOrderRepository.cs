using API.Domain.Models;
using API.Domain.DTOs;

namespace API.Repository.interfaces
{
    public interface IOrderRepository
    {
        OrderModel InsertOrder(OrderModel order);
        String InsertPayment(OrderPaymentDTO dto);
        List<OrderModel> GetAll();
        OrderModel FindAndUpdate(OrderModel order);
        OrderModel GetById(Guid Id);
        String FindAndDelete(GetGuidDTO dto);
    }
}