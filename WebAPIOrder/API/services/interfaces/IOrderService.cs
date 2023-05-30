using API.Domain.DTOs;
using API.Domain.Models;

namespace API.services.interfaces
{
    public interface IOrderService
    {
        ResponseDTO InsertNewOrder(OrderProductDTO dto);
        List<OrderModel> GetAllOrders();
        OrderDTO GetOrderById(Guid Id);
        ResponseDTO FindAndUpdateOrder(OrderProductDTO dto);
    }
}