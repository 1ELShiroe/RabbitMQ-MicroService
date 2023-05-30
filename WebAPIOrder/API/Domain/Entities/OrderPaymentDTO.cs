using API.Domain.Models;

namespace API.Domain.DTOs
{
    public class OrderPaymentDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required Guid OrderId { get; set; }
        public required Guid CustomerId { get; set; }
        public required Decimal Amount { get; set; }
        public String? Status { get; set; } = null!;
        public DateTime? Date { get; set; } = null!;
        public required String Method { get; set; }
        public CardModel? Card { get; set; } = null!;
    }
}