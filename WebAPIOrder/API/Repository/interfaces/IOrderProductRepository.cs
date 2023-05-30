using API.Domain.Models;
using API.Domain.DTOs;

namespace API.Repository.interfaces
{
    public interface IOrderProductRepository
    {
        OrderProductModel InsertOrderProduct(OrderProductModel product);
        List<OrderProductModel> GetOrders(Guid Id);
        void FindAndRemove(Guid Id);
    }
}