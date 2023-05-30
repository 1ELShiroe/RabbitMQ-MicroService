namespace API.Domain.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public required String? Name { get; set; }
        public required Decimal Value { get; set; }
        public required Int32 Quantity { get; set; }
    }
}