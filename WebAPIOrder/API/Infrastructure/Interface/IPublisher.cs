using API.Domain.DTOs;

namespace API.Infrastructure.Interface
{
    public interface IPublisher
    {
        void Publish(OrderPaymentDTO dto, String queue);
    }
}