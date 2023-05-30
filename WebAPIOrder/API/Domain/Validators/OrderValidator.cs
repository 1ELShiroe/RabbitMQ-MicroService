using API.Domain.Models;
using FluentValidation;

namespace API.Domain.Validators
{
    public class OrderValidator : AbstractValidator<OrderModel>
    {
        public OrderValidator()
        {
            RuleFor(o => o.ClientId)
                .NotNull()
                    .WithMessage("it was not possible to create an order, because the customer is invalid");

            RuleFor(o => o.OrderProduct)
                .NotNull()
                    .WithMessage("invalid order, no product within the order");
        }
    }
}