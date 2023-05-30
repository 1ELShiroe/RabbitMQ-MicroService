using API.Infrastructure.Context;
using API.Domain.Models;
using API.Domain.DTOs;
using API.Repository.interfaces;

namespace API.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationContext _context;
        public PaymentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public String InsertPayment(OrderPaymentDTO dto)
        {
            try
            {
                // Check if customer and order exist
                var customer = _context.Customers.FirstOrDefault(c => c.Id == dto.CustomerId);
                var order = _context.Orders.FirstOrDefault(o => o.Id == dto.OrderId);

                if (customer is null)
                {
                    return $"there was an error processing the payment, user ID: {dto.CustomerId} not found";
                }
                if (order is null)
                {
                    return $"there was an error processing payment, order with ID: {dto.OrderId} not found";
                }

                if (order.Payment is not null)
                {
                    return $"there is already a payment assigned to the order with ID: {dto.OrderId}";
                }

                // Save payment on order
                var payment = new PaymentOrder()
                {
                    Id = Guid.NewGuid(),
                    CustomerId = dto.CustomerId,
                    OrderId = dto.OrderId,
                    Method = dto.Method,
                    Status = dto.Status!,
                    Date = DateTime.Now.ToUniversalTime()
                };

                _context.Payments.Add(payment);
                order.Payment = payment.Id;

                _context.SaveChangesAsync();

                return $"payment registered successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }
    }
}