using API.Controllers;
using API.Infrastructure.Context;
using API.Domain.Models;
using API.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Tests.Controllers
{
    [UseAutofacTestFramework]
    public class ProductControllerTests
    {
        private readonly ProductController _productController;
        private readonly ApplicationContext _context;
        public ProductControllerTests(ProductController productController, ApplicationContext context)
        {
            _productController = productController;
            _context = context;
        }
        [Fact]
        public void ProductControllerAndTestsRouterRegister()
        {
            var productDTO = new ProductDTO()
            {
                Name = "Vaso",
                Value = (Decimal)20.3,
                Quantity = 5
            };

            var product = _productController.Create(productDTO);
            product.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ProductControllerAndTestsRouterUpdate()
        {
            var productDTO = new ProductDTO()
            {
                Name = "Vaso",
                Value = (Decimal)25.3,
                Quantity = 10
            };

            var product = _productController.Create(productDTO);
            product.Should().BeOfType<OkObjectResult>();

            productDTO.Quantity = 20;
            var UpdateProduct = _productController.Update(productDTO);
            product.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void ProductControllerAndTestsRouterDelete()
        {

            var productId = new GetGuidDTO()
            {
                Id = Guid.NewGuid()
            };

            var res = _productController.Delete(productId);
            res.Should().BeOfType<NotFoundObjectResult>();

            var newProduct = new ProductModel()
            {
                Id = Guid.NewGuid(),
                Name = "FakeProduct",
                Value = (Decimal)50.0,
                Quantity = 2
            };

            var createProduct = _context.Products.Add(newProduct);

            var res2 = _productController.Delete(
                new GetGuidDTO()
                {
                    Id = newProduct.Id
                }
             );

            res2.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public void ProductControllerAndTestsRouterFindById()
        {
            var productId = new GetGuidDTO()
            {
                Id = Guid.NewGuid()
            };

            var res = _productController.FindById(productId);
            res.Should().BeOfType<NotFoundObjectResult>();

            var newProduct = new ProductModel()
            {
                Id = Guid.NewGuid(),
                Name = "FakeProduct",
                Value = (Decimal)50.0,
                Quantity = 2
            };

            var createProduct = _context.Products.Add(newProduct);

            productId.Id = newProduct.Id;
            var res1 = _productController.FindById(productId);
            res1.Should().BeOfType<OkObjectResult>();
        }
    }
}