using API.Controllers;
using API.Domain.Models;
using API.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Tests.Controllers
{
    [UseAutofacTestFramework]
    public class CustomerControllerTests
    {
        private readonly CustomerController _controller;
        public CustomerControllerTests(CustomerController controller)
        {
            _controller = controller;
        }

        [Fact]
        public void CustomerControllerAndTestsRouterRegister()
        {
            var customerDTO = new CustomerModel()
            {
                Name = "Jacson",
                Password = "123456789"
            };

            var rgCustomer = _controller!.Register(customerDTO);
            rgCustomer.Should().BeOfType<OkObjectResult>();

            var rgCustomer2 = _controller!.Register(new CustomerModel()
            {
                Name = "Jacson",
                Password = "15666"
            });

            rgCustomer2.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public void CustomerControllerAndTestsRouterLogin()
        {
            var customerDTO = new CustomerModel()
            {
                Name = "Jacson",
                Password = "123456789"
            };

            var registerCustomer = _controller!.Register(customerDTO);
            registerCustomer.Should().BeOfType<NotFoundObjectResult>();

            var loginCustomer = _controller!.Login(customerDTO);
            loginCustomer.Should().BeOfType<OkObjectResult>();

            var newCustomerDTO = new CustomerModel()
            {
                Name = "Adriana",
                Password = "aleatoria"
            };

            var loginCustomer2 = _controller!.Login(newCustomerDTO);
            loginCustomer2.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public void CustomerControllerAndTestsRouterChangePassword()
        {
            var customerDTO = new CustomerModel()
            {
                Name = "Jacson",
                Password = "123456789"
            };

            var registerCustomer = _controller!.Register(customerDTO);
            registerCustomer.Should().BeOfType<NotFoundObjectResult>();


            var loginCustomer = _controller!.ChangePassword(
                new CustomerDTO()
                {
                    Name = "Jacson",
                    Password = "123456789",
                    Changepassword = "asdasd5646565"
                }
            );
            loginCustomer.Should().BeOfType<OkObjectResult>();
        }
    }
}