using API.Infrastructure.Context;
using API.Repository.interfaces;
using API.Domain.DTOs;
using API.Domain.Models;
using API.services;

namespace API.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationContext _context;

        public CustomerRepository(ApplicationContext context)
        {
            _context = context;
        }
        public CustomerInfoDTO CreateCustomer(CustomerModel customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return new CustomerInfoDTO()
            {
                Id = customer.Id,
                Name = customer.Name,
                Role = customer.Role
            };
        }    
        public CustomerInfoDTO FindCustomer(CustomerModel customers)
        {
            var customerRepository = _context.Customers.Where(i =>
                 i.Name!.ToLower() == customers.Name!.ToLower() &&
                 i.Password == customers.Password
                );

            if (customerRepository.ToList().Count >= 1)
            {
                var customer = customerRepository.DefaultIfEmpty().Single();

                return new CustomerInfoDTO()
                {
                    Id = customer!.Id,
                    Name = customer.Name,
                    Role = customer.Role
                };
            }
            return null!;
        }
        public List<CustomerInfoDTO> FindCustomerAll()
        {
            List<CustomerModel> customers = _context.Customers.ToList();
            var newList = new List<CustomerInfoDTO>();

            customers.ForEach(item =>
                newList.Add(new CustomerInfoDTO
                {
                    Id = item?.Id,
                    Name = item?.Name,
                    Role = item?.Role
                })
            );
            return newList;
        }
        public CustomerInfoDTO GetCustomerById(Guid Id)
        {
            var customer = _context.Customers.Find(Id);
            return new CustomerInfoDTO
            {
                Id = customer?.Id,
                Name = customer?.Name,
                Role = customer?.Role
            };
        }
        public String FindAndDelete(GetGuidDTO dto)
        {
            var customer = _context.Customers.Where(i => i.Id == dto.Id)
                    .DefaultIfEmpty().Single();
            Console.WriteLine(customer);
            if (customer == null)
            {
                return "No user found with given ID.";
            }

            _context.Customers.Remove(customer!);
            _context.SaveChanges();

            return "user deleted successfully.";
        }
        public String FindAndUpdate(CustomerDTO dto)
        {
            var customer = _context.Customers.Find(dto.Id);

            if (customer is not null)
            {
                customer.Password = dto.Changepassword;
                _context.SaveChanges();

                return "password changed successfully.";
            }

            return "user not found.";
        }
    }
}