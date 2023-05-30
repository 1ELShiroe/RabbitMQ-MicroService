using API.Domain.DTOs;

namespace API.services.interfaces
{
    public interface IProductService
    {
        ProductDTO FindAndUpdate(ProductDTO dto);
        List<ProductDTO> FindProductAll();
        ResponseDTO FindAndDelete(GetGuidDTO dto);
        ResponseDTO Create(ProductDTO dto);
        ResponseDTO FindById(Guid Id);
    }
}