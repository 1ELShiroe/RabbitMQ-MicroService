using API.Infrastructure.Context;
using API.Repository.interfaces;
using API.Domain.Models;
using API.Domain.DTOs;

namespace API.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;
        private readonly IOrderProductRepository _dbOrder;
        private readonly IProductRepository _dbProduct;
        public OrderRepository(
            ApplicationContext context,
            IOrderProductRepository dbOrder,
            IProductRepository dbProduct
           )
        {
            _context = context;
            _dbOrder = dbOrder;
            _dbProduct = dbProduct;
        }

        public OrderModel InsertOrder(OrderModel order)
        {
            var data = _context.Orders.Add(order);
            _context.SaveChanges();

            return new OrderModel()
            {
                Id = order.Id,
                Amount = order.Amount,
                ClientId = order.ClientId,
                OrderProduct = order.OrderProduct,
                Cupom = order.Cupom
            };
        }
        public String InsertPayment(OrderPaymentDTO dto)
        {
            var orderFindDb = _context.Orders.FirstOrDefault(c => c.Id == dto.OrderId);

            if (orderFindDb is not null)
            {
                orderFindDb.Payment = dto.Id;
                _context.SaveChanges();

                return $"payment entered successfully";
            }

            return $"error finding order with ID: {dto.OrderId}";
        }
        public OrderModel FindAndUpdate(OrderModel order)
        {
            var orderFindDb = _context.Orders.Find(order.Id);
            if (orderFindDb is not null)
            {
                orderFindDb.Amount = order.Amount;
                orderFindDb.Cupom = order.Cupom;
                orderFindDb.OrderProduct = order.OrderProduct;

                _context.SaveChanges();

                return orderFindDb;
            }

            return null!;
        }
        public OrderModel GetById(Guid Id)
        {
            return _context.Orders.Find(Id) ?? null!;
        }
        public List<OrderModel> GetAll()
        {
            List<OrderModel> orderList = _context.Orders.ToList();

            return orderList;
        }
        public String FindAndDelete(GetGuidDTO dto)
        {
            var validProduct = _context.Orders.Where(i => i.Id == dto.Id)
                    .DefaultIfEmpty()
                    .Single();

            if (validProduct == null)
            {
                return "no order found with the given ID.";
            }

            _context.Orders.Remove(validProduct);
            _context.SaveChanges();

            return "order deleted successfully.";
        }
    }
}