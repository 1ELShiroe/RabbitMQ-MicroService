namespace PaymentAPI.Domain.Entities
{
    public class OrderCreateDTO
    {
        public Guid Id { get; set; }
        public required Guid OrderId { get; set; }
        public required Guid CustomerId { get; set; }
        public required Decimal Amount { get; set; }
        public required String Method { get; set; }
        public String Status { get; set; } = null!;
        public CardModel? Card { get; set; } = null!;
    }
}