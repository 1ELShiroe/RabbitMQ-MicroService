using API.Domain.Models;

namespace API.Domain.DTOs
{
    public class ResponseDTO
    {
        public Boolean Error { get; set; }
        public String Message { get; set; } = null!;
        public OrderDTOResponse Order { get; set; } = null!;
        public CustomerInfoDTO Customer { get; set; } = null!;
        public ProductModel Product { get; set; } = null!;
    }
}