using API.Domain.Models;
using API.Domain.DTOs;
using API.services.interfaces;

namespace Tests.Services
{
    [UseAutofacTestFramework]
    public class OrderServiceTests
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        public OrderServiceTests(
            IOrderService orderService, ICustomerService customerService, IProductService productService
            )
        {
            _orderService = orderService;
            _productService = productService;
            _customerService = customerService;
        }

        [Fact]
        public void CreateOrder()
        {
            var newCustomer = _customerService.Register(new CustomerModel()
            {
                Name = "Andian",
                Password = "123456789",
            });

            var products = new List<ProductDTO>();

            products.Add(new ProductDTO() { Name = "Jarro de Barro", Value = (Decimal)10.5, Quantity = 5 });
            products.Add(new ProductDTO() { Name = "Lapis", Value = (Decimal)23.5, Quantity = 2 });
            products.Add(new ProductDTO() { Name = "Caderno", Value = (Decimal)11.5, Quantity = 3 });
            products.Add(new ProductDTO() { Name = "Estojo", Value = (Decimal)7.5, Quantity = 1 });

            var orderProducts = new List<OrderCreateDTO>();

            foreach (var product in products)
            {
                var dbProduct = _productService.Create(product);
                orderProducts.Add(
                    new OrderCreateDTO()
                    {
                        Id = dbProduct.Product.Id,
                        Value = dbProduct.Product.Value,
                        Quantity = dbProduct.Product.Quantity
                    }
                );
            }

            var data = _orderService.InsertNewOrder(
                new OrderProductDTO()
                {
                    CustomerId = (Guid)newCustomer.Id!,
                    ProductId = orderProducts,
                }
            );

            data.Should().BeOfType<ResponseDTO>();
            data.Message.Should().BeOfType<String>("request registered successfully.");
            data.Order.Products.Should().HaveCount(4);
        }

        [Fact]
        public void OrderServiceFindByIdAndTests()
        {
            var data = _orderService.GetOrderById(Guid.NewGuid());
            data.Should().BeNull();
        }

    }
}