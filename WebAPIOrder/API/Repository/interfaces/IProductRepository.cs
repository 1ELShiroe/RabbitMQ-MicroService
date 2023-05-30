using API.Domain.DTOs;
using API.Domain.Models;

namespace API.Repository.interfaces
{
    public interface IProductRepository
    {
        List<ProductDTO> GetAll();
        ProductDTO GetProductById(Guid Id);
        void Create(ProductModel product);
        String Update(ProductModel oldProduct, ProductDTO newProduct);
        void Delete(ProductModel product);
        ProductModel FindById(Guid dto);
    }
}