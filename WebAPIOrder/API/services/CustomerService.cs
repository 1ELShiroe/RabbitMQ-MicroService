using API.Repository.interfaces;
using API.services.interfaces;
using API.Domain.Models;
using API.Domain.DTOs;

namespace API.services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public ResponseDTO Login(CustomerModel customers)
        {
            var customer = _customerRepository.FindCustomer(customers);
            if (customer is not null)
            {
                return new ResponseDTO()
                {
                    Error = false,
                    Customer = customer
                };
            }

            return new ResponseDTO()
            {
                Error = true,
                Message = "user not found."
            };
        }
        public CustomerInfoDTO Register(CustomerModel customer)
        {
            List<CustomerInfoDTO> customers = _customerRepository.FindCustomerAll();

            if (customers.Exists(x => x.Name == customer.Name))
            {
                return null!;
            }
            return _customerRepository.CreateCustomer(customer);

        }
        public List<CustomerInfoDTO> GetAll()
        {
            return _customerRepository.FindCustomerAll();
        }
        public String ChangePassword(CustomerDTO dto)
        {
            var customer = _customerRepository.FindCustomer(new CustomerModel()
            {
                Name = dto.Name!,
                Password = dto.Password
            });

            if (customer.Name is null)
            {
                return "no users found.";
            }
            if (dto.Password == dto.Changepassword)
            {
                return "the new password entered is the same as the old one.";
            }
            if (dto.Changepassword!.Length < 8)
            {
                return "Password entered with less than 8 characters.";
            }

            return "password changed successfully.";
        }
        public CustomerInfoDTO GetCustomerById(Guid Id)
        {
            return _customerRepository.GetCustomerById(Id);
        }
        public String FindAndDelete(GetGuidDTO dto)
        {
            return _customerRepository.FindAndDelete(dto);
        }
    }
}