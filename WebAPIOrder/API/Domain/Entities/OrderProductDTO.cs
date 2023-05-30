namespace API.Domain.DTOs
{
    public class OrderProductDTO
    {
        public Guid Id { get; set; }
        public required Guid CustomerId { get; set; }
        public required List<OrderCreateDTO> ProductId { get; set; }
        public String? Cupom { get; set; }
        public Decimal Amount { get; set; }
    }
}