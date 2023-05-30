using PaymentAPI.Domain.Entities;

namespace PaymentAPI.infrastructure.Interfaces
{
    public interface IPublisher
    {
        void Publish(OrderCreateDTO dto, String queue);
    }
}