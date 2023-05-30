using API.Domain.Models;
using API.Domain.DTOs;
using API.services.interfaces;

namespace Tests.Services
{
    [UseAutofacTestFramework]
    public class CustomerServiceTests
    {
        private readonly ICustomerService _customerService;
        public CustomerServiceTests(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [Fact]
        public void CustomerServiceRegisterAndTests()
        {
            var customer = new CustomerModel()
            {
                Name = "Andre",
                Password = "123456789"
            };

            var newCustomer = _customerService.Register(customer);

            newCustomer.Should().NotBeNull();
            newCustomer.Name.Should().NotBeNull().And.Be("Andre");
        }

        [Fact]
        public void CustomerServiceLoginAndTests()
        {
            var customer = new CustomerModel()
            {
                Name = "Lucas",
                Password = "123456789"
            };
            var newCustomer = _customerService.Register(customer);

            newCustomer.Should().NotBeNull();
            newCustomer.Name.Should().NotBeNull().And.Be("Lucas");

            var loginCustomer = _customerService.Login(customer);
            loginCustomer.Should().NotBeNull();
        }

        [Fact]
        public void CustomerServiceChangePasswordAndTests()
        {
            var customer = new CustomerModel()
            {
                Name = "Raul",
                Password = "123456789"
            };

            _customerService.Register(customer);

            var customerChange = _customerService.ChangePassword(
                new CustomerDTO()
                {
                    Name = "Raul",
                    Password = "123456789",
                    Changepassword = "5555"
                }
            );

            customerChange.Should().Be("Password entered with less than 8 characters.");

            customerChange = _customerService.ChangePassword(
                new CustomerDTO()
                {
                    Name = "Raul",
                    Password = "123456789",
                    Changepassword = "555555555555"
                }
            );

            customerChange.Should().Be("password changed successfully.");
        }
    }
}