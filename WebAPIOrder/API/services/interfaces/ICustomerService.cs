using API.Domain.DTOs;
using API.Domain.Models;

namespace API.services.interfaces
{
    public interface ICustomerService
    {
        CustomerInfoDTO Register(CustomerModel customer);
        public ResponseDTO Login(CustomerModel customer);
        String ChangePassword(CustomerDTO dto);
        String FindAndDelete(GetGuidDTO dto);
        List<CustomerInfoDTO> GetAll();
        CustomerInfoDTO GetCustomerById(Guid Id);
    }
}