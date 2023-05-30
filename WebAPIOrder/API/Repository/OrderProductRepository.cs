using API.Infrastructure.Context;
using API.Domain.Models;
using API.Domain.DTOs;
using API.Repository.interfaces;

namespace API.Repository
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly ApplicationContext _context;
        public OrderProductRepository(ApplicationContext context)
        {
            _context = context;
        }
        public OrderProductModel InsertOrderProduct(OrderProductModel product)
        {
            _context.OrderProduct.Add(product);
            _context.SaveChanges();

            return product;
        }
        public List<OrderProductModel> GetOrders(Guid Id)
        {
            List<OrderProductModel> orderProduct = _context.OrderProduct
                                .Where(p => p.OrderId == Id)
                                .ToList();

            return orderProduct;
        }
        public void FindAndRemove(Guid Id)
        {
            List<OrderProductModel> orderProduct = _context.OrderProduct
                                .Where(p => p.OrderId == Id)
                                .ToList();

            _context.OrderProduct.RemoveRange(orderProduct);
            _context.SaveChanges();
        }
    }
}