using API.Domain.DTOs;
using API.Domain.Models;

namespace API.Repository.interfaces
{
    public interface ICustomerRepository
    {
        List<CustomerInfoDTO> FindCustomerAll();
        CustomerInfoDTO CreateCustomer(CustomerModel customer);
        CustomerInfoDTO FindCustomer(CustomerModel customer);
        CustomerInfoDTO GetCustomerById(Guid Id);
        String FindAndDelete(GetGuidDTO dto);
        String FindAndUpdate(CustomerDTO dto);
    }
}