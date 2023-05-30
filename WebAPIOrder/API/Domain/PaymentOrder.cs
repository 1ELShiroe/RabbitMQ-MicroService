namespace API.Domain.Models
{
    public class PaymentOrder
    {
        public Guid Id { get; set; }
        public required Guid OrderId { get; set; }
        public required Guid CustomerId { get; set; }
        public required String Status { get; set; }
        public required String Method { get; set; }
        public required DateTime Date { get; set; }
    }
}