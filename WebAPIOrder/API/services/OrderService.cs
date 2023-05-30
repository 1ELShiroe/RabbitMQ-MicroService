using API.Repository.interfaces;
using API.services.interfaces;
using API.Domain.Models;
using API.Domain.DTOs;

namespace API.services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly ICustomerRepository _customerRepository;


        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IOrderProductRepository orderProductRepository,
            ICustomerRepository customerRepository
            )
        {
            _orderProductRepository = orderProductRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public List<OrderModel> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }
        public ResponseDTO InsertNewOrder(OrderProductDTO dto)
        {
            Decimal amount = 0;
            var customer = _customerRepository.GetCustomerById(dto.CustomerId);

            if (customer.Name is null)
            {
                return new ResponseDTO()
                {
                    Error = true,
                    Message = "no user found with given id."
                };
            }

            foreach (var productId in dto.ProductId)
            {
                var productDTO = _productRepository.FindById(productId.Id);
                if (productDTO is null)
                {
                    return new ResponseDTO()
                    {
                        Error = true,
                        Message = $"no product with ID: {productId.Id} was found"
                    };
                }

                amount += (productId.Value * productId.Quantity);
            }

            if (dto.Cupom == "BLACK FRAUDE")
            {
                var calc = amount * (Decimal)(20 / 100.0);
                amount = calc;
            }
            else
            {
                dto.Cupom = null!;
            }

            var order = new OrderModel()
            {
                Id = Guid.NewGuid(),
                ClientId = dto.CustomerId,
                Amount = amount,
                Cupom = dto.Cupom
            };

            _orderRepository.InsertOrder(order);

            var products = new List<ProductDTO>();

            foreach (var productId in dto.ProductId)
            {
                var productDTO = _productRepository.FindById(productId.Id);
                var orderProduct = new OrderProductModel
                {
                    OrderId = order.Id,
                    ProductId = productId.Id,
                    Amount = (productId.Value * productId.Quantity),
                    Quantity = productId.Quantity
                };

                _orderProductRepository.InsertOrderProduct(orderProduct);

                var product = _productRepository.FindById(productId.Id);

                products.Add(
                    new ProductDTO()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Value = product.Value,
                        Quantity = product.Quantity
                    }
                );
            }

            return new ResponseDTO()
            {
                Error = false,
                Message = "request registered successfully.",
                Order = new OrderDTOResponse()
                {
                    Id = order.Id,
                    Customer = customer,
                    Products = products,
                    Amount = amount
                }
            };
        }
        public OrderDTO GetOrderById(Guid Id)
        {
            var order = _orderRepository.GetById(Id);
            if (order is not null)
            {
                List<OrderProductModel> listOrderProduct = _orderProductRepository.GetOrders(Id);

                var products = new List<ProductDTO>();

                decimal ammout = 0;

                listOrderProduct.ForEach(item =>
                {
                    ProductDTO product = _productRepository.GetProductById(item.ProductId);
                    product.Value = item.Amount;
                    product.Quantity = item.Quantity;
                    ammout += item.Amount;

                    products.Add(product);
                });

                var customer = _customerRepository.GetCustomerById(order.ClientId);

                return new OrderDTO()
                {
                    Id = order.Id,
                    products = products,
                    customer = customer,
                    Amount = ammout,
                };
            }
            return null!;
        }
        public ResponseDTO FindAndUpdateOrder(OrderProductDTO dto)
        {
            Decimal amount = 0;
            var order = this.GetOrderById(dto.Id);

            if (order is null)
            {
                return new ResponseDTO()
                {
                    Error = true,
                    Message = "order not found."
                };
            };

            _orderProductRepository.FindAndRemove(dto.Id);

            // List<OrderCreateDTO> newList = dto.ProductId.ToList().FindAll(x => x.Quantity != 0);
            foreach (var productId in dto.ProductId)
            {
                var productDTO = _productRepository.FindById(productId.Id);

                if (productDTO is null)
                {
                    return new ResponseDTO()
                    {
                        Error = true,
                        Message = $"no product with ID: {productId.Id} was found"
                    };
                }

                amount += (productId.Value * productId.Quantity);
            }

            if (dto.Cupom == "BLACK FRAUDE")
            {
                var calc = amount * (Decimal)(20 / 100.0);
                amount = calc;
            }
            else
            {
                dto.Cupom = null!;
            }

            var newOrder = new OrderModel()
            {
                Id = dto.Id,
                ClientId = dto.CustomerId,
                Amount = amount,
                Cupom = dto.Cupom
            };


            foreach (var productId in dto.ProductId)
            {
                var orderProduct = new OrderProductModel
                {
                    OrderId = dto.Id,
                    ProductId = productId.Id,
                    Amount = (productId.Value * productId.Quantity),
                    Quantity = productId.Quantity,
                };

                _orderProductRepository.InsertOrderProduct(orderProduct);
            }

            var updateOrder = _orderRepository.FindAndUpdate(newOrder);

            if (updateOrder is not null)
            {
                return new ResponseDTO()
                {
                    Error = false,
                    Message = "successful change.",
                    Order = new OrderDTOResponse()
                    {
                        Id = newOrder.Id,
                        ProductIDs = dto.ProductId,
                        Customer = order.customer,
                        Amount = amount
                    }
                };
            }

            return new ResponseDTO()
            {
                Error = true,
                Message = $"no order found with ID: {newOrder.Id}."
            };
        }
    }
}
