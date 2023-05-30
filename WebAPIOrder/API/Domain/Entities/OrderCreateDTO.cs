namespace API.Domain.DTOs
{
    public class OrderCreateDTO
    {
        public Guid Id { get; set; }
        public Int32 Quantity { get; set; }
        public Decimal Value { get; set; }
    }
}