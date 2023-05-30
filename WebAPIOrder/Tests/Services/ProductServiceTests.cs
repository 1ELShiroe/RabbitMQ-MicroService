using API.Domain.Models;
using API.Domain.DTOs;
using API.services.interfaces;

namespace Tests.Services
{
    [UseAutofacTestFramework]
    public class ProductServiceTests
    {
        private readonly IProductService _productService;
        public ProductServiceTests(IProductService productService)
        {
            _productService = productService;
        }

        [Fact]
        public void FindProductById()
        {
            var product = _productService.Create(new ProductDTO()
            {
                Name = "Vaso",
                Value = (Decimal)20.2,
                Quantity = 5
            });

            product.Should().NotBeNull();
            product.Product.Name.Should().NotBeNull();

            var newProduct = new ProductDTO()
            {
                Id = product.Product.Id,
                Name = product.Product.Name,
                Value = product.Product.Value,
                Quantity = product.Product.Quantity
            };

            newProduct.Name.Should().Be("Vaso");

            var findProduct = _productService.FindById(newProduct.Id);
            findProduct.Should().NotBeNull();
            findProduct.Error.Should().Be(false);
        }
        [Fact]
        public void FindByIdAndDeleteProductTests()
        {
            var product = _productService.Create(
                new ProductDTO()
                {
                    Name = "Vaso",
                    Value = (Decimal)20.2,
                    Quantity = 5
                }
            );

            product.Should().NotBeNull();
            product.Product.Name.Should().NotBeNull();

            var newProduct = new ProductDTO()
            {
                Id = product.Product.Id,
                Name = product.Product.Name,
                Value = product.Product.Value,
                Quantity = product.Product.Quantity
            };

            newProduct.Name.Should().Be("Vaso");

            var findProduct = _productService.FindById(newProduct.Id);
            findProduct.Should().NotBeNull();
            findProduct.Error.Should().Be(false);

            var productDelete = _productService.FindAndDelete(
                new GetGuidDTO()
                {
                    Id = findProduct.Product.Id
                }
            );

            productDelete.Should().NotBeNull()
                .And.BeOfType<ResponseDTO>();
        }

        [Fact]
        public void CreateProductAndUpdateProduct()
        {
            var product = _productService.Create(
                new ProductDTO()
                {
                    Name = "Vaso",
                    Value = (Decimal)20.2,
                    Quantity = 5
                }
            );

            product.Should().NotBeNull();
            product.Product.Name.Should().NotBeNull();

            var newProduct = new ProductDTO()
            {
                Id = product.Product.Id,
                Name = product.Product.Name,
                Value = product.Product.Value,
                Quantity = product.Product.Quantity
            };

            newProduct.Name.Should().Be("Vaso");

            var updateProduct = _productService.FindAndUpdate(
                new ProductDTO()
                {
                    Id = newProduct.Id,
                    Name = "Vaso Porcelana",
                    Value = (Decimal)50.99,
                    Quantity = 2
                }
            );

            updateProduct.Name.Should().Be("Vaso Porcelana");
            updateProduct.Quantity.Should().Be(2);
        }
    }
}