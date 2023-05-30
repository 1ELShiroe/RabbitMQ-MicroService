using PaymentAPI.Domain.Entities;

namespace PaymentAPI.infrastructure.Interfaces
{

    public interface IConsumer
    {
        Task Listening(OrderCreateDTO @event);
    }
}