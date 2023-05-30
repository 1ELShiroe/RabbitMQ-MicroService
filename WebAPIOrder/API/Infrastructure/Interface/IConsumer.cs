
using API.Domain.DTOs;

namespace API.Infrastructure.Interface
{
    public interface IConsumer
    {
        Task Listening(OrderPaymentDTO @event);
    }
}