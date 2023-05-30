using API.Domain.DTOs;
using API.Domain.Models;
using API.Repository.interfaces;
using API.services.interfaces;

namespace API.services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductDTO FindAndUpdate(ProductDTO dto)
        {
            var oldProcut = _productRepository.FindById(dto.Id);

            if (oldProcut is not null)
            {
                _productRepository.Update(oldProcut, dto);
                return dto;
            }
            return null!;
        }
        public List<ProductDTO> FindProductAll()
        {
            return _productRepository.GetAll();
        }
        public ResponseDTO FindAndDelete(GetGuidDTO dto)
        {
            var product = _productRepository.FindById(dto.Id);

            if (product is not null)
            {
                _productRepository.Delete(product);

                return new ResponseDTO()
                {
                    Error = false,
                    Message = "product deleted successfully."
                };
            }
            return new ResponseDTO()
            {
                Error = true,
                Message = $"no product found with ID: {dto.Id}"
            };
        }
        public ResponseDTO Create(ProductDTO dto)
        {
            if (dto.Name is not null)
            {
                var procutNew = new ProductModel()
                {
                    Name = dto.Name,
                    Value = dto.Value,
                    Quantity = dto.Quantity
                };

                _productRepository.Create(procutNew);
                return new ResponseDTO()
                {
                    Error = false,
                    Product = procutNew
                };
            }
            return new ResponseDTO()
            {
                Error = true,
                Message = "procutNe"
            };
        }
        public ResponseDTO FindById(Guid Id)
        {
            var product = _productRepository.FindById(Id);
            if (product is not null)
            {
                return new ResponseDTO()
                {
                    Error = false,
                    Message = "product found successfully.",
                    Product = product
                };
            }
            return new ResponseDTO()
            {
                Error = true,
                Message = $"product with tough ID: {Id} not found."
            };
        }
    }
}