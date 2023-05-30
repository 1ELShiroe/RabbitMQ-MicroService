namespace API.Domain.DTOs
{
    public class OrderDTOResponse
    {
        public required Guid Id { get; set; }
        public CustomerInfoDTO? Customer { get; set; }
        public required Decimal Amount { get; set; }
        public List<ProductDTO>? Products { get; set; }
        public List<OrderCreateDTO>? ProductIDs { get; set; }
    }
}