using API.Controllers;
using API.Infrastructure.Context;
using API.Domain.Models;
using API.Domain.DTOs;

namespace Tests.Controllers
{
    [UseAutofacTestFramework]
    public class OrderControllerTests
    {
        private readonly OrderController _orderController;
        private readonly ApplicationContext _context;
        public OrderControllerTests(OrderController orderController, ApplicationContext context)
        {
            _orderController = orderController;
            _context = context;
        }

        [Fact]
        public void OrderControllerAndTestsRouterCreate()
        {
            var products = new List<OrderCreateDTO>();
            products.Add(new OrderCreateDTO() { Id = Guid.NewGuid(), Quantity = 1, Value = (Decimal)20 });
            products.Add(new OrderCreateDTO() { Id = Guid.NewGuid(), Quantity = 7, Value = (Decimal)45.1 });
            products.Add(new OrderCreateDTO() { Id = Guid.NewGuid(), Quantity = 3, Value = (Decimal)2.5 });

            var newOrder = new OrderProductDTO()
            {
                ProductId = products,
                CustomerId = Guid.NewGuid()
            };

            var res = _orderController.Create(newOrder);
            res.Should().NotBeNull()
                .And.BeOfType<NotFoundObjectResult>();

            var newProduct = new ProductModel()
            {
                Id = Guid.NewGuid(),
                Name = "FakeProduct",
                Value = (Decimal)20.1,
                Quantity = 5
            };

            var newCustomer = new CustomerModel()
            {
                Id = Guid.NewGuid(),
                Name = "ELShiroe",
                Password = "123456789"
            };

            _context.Products.Add(newProduct);
            _context.Customers.Add(newCustomer);


            products.Clear();

            products.Add(new OrderCreateDTO() { Id = newProduct.Id, Quantity = newProduct.Quantity, Value = newProduct.Value });
            newOrder.CustomerId = newCustomer.Id;

            var res1 = _orderController.Create(newOrder);
            res1.Should().NotBeNull().And.BeOfType<OkObjectResult>();
        }

        [Fact]
        public void OrderControllerAndTestsRouterFindById()
        {
            var getID = new GetGuidDTO()
            {
                Id = Guid.NewGuid()
            };

            var res = _orderController.FindById(getID);
            res.Should().NotBeNull()
                .And.BeOfType<NotFoundObjectResult>();



            var newProduct = new ProductModel()
            {
                Id = Guid.NewGuid(),
                Name = "FakeProduct",
                Value = (Decimal)20.1,
                Quantity = 5
            };

            var newCustomer = new CustomerModel()
            {
                Id = Guid.NewGuid(),
                Name = "ELShiroe",
                Password = "123456789"
            };

            _context.Products.Add(newProduct);
            _context.Customers.Add(newCustomer);

            var products = new List<OrderCreateDTO>();

            products.Add(new OrderCreateDTO() { Id = newProduct.Id, Quantity = newProduct.Quantity, Value = newProduct.Value });

            var newOrder = new OrderProductDTO()
            {
                ProductId = products,
                CustomerId = newCustomer.Id
            };

            var res1 = _orderController.Create(newOrder);
            res1.Should().NotBeNull()
                .And.BeOfType<OkObjectResult>();
        }

        [Fact]
        public void OrderControllerAndTestsRouterUpdate()
        {
            var newProduct = new ProductModel() { Id = Guid.NewGuid(), Name = "FakeProduct", Value = (Decimal)20.1, Quantity = 5 };
            var newCustomer = new CustomerModel() { Id = Guid.NewGuid(), Name = "ELShiroe", Password = "123456789" };

            _context.Products.Add(newProduct);
            _context.Customers.Add(newCustomer);

            var products = new List<OrderCreateDTO>();
            products.Add(new OrderCreateDTO() { Id = newProduct.Id, Quantity = newProduct.Quantity, Value = newProduct.Value });

            var newOrder = new OrderProductDTO()
            {
                Id = Guid.NewGuid(),
                ProductId = products,
                CustomerId = newCustomer.Id
            };

            var res = _orderController.Create(newOrder);
            res.Should().NotBeNull()
                .And.BeOfType<OkObjectResult>();

            if (res is OkObjectResult okResult)
            {
                var orderDTO = okResult.Value as ResponseDTO;

                products.Clear();
                var product = new OrderCreateDTO() { Id = newProduct.Id, Quantity = 700, Value = newProduct.Value };
                products.Add(product);

                var newOrderV2 = new OrderProductDTO()
                {
                    Id = Guid.NewGuid(),
                    ProductId = products,
                    CustomerId = newCustomer.Id
                };

                var res1 = _orderController.FindAndUpdate(newOrderV2);
                res1.Should().NotBeNull()
                    .And.BeOfType<NotFoundObjectResult>();

                newOrderV2.Id = orderDTO!.Order.Id;
                var res2 = _orderController.FindAndUpdate(newOrderV2);
                res2.Should().NotBeNull()
                    .And.BeOfType<OkObjectResult>();

                var data = res2;

                if (res2 is OkObjectResult getUpdateRes)
                {
                    var orderResponseUpdateOK = getUpdateRes.Value as ResponseDTO;
                    if (orderResponseUpdateOK is not null)
                    {
                        orderResponseUpdateOK.Order.Amount.Should().Be(product.Quantity * product.Value);
                    }
                }
            }
        }
    }
}