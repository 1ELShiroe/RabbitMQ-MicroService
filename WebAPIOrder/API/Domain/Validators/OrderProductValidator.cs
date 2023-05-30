using API.Domain.Models;
using FluentValidation;

namespace API.Domain.Validators
{
    public class OrderProductValidator : AbstractValidator<OrderProductModel>
    {
        public OrderProductValidator()
        {
            RuleFor(op => op.OrderId)
                .NotNull()
                    .WithMessage("order id cannot be null");

            RuleFor(op => op.ProductId)
                .NotNull()
                    .WithMessage("product id cannot be null");

            RuleFor(op => op.Quantity)
                .NotNull()
                    .WithMessage("quantity cannot be null")
                .GreaterThan(0)
                    .WithMessage("quantity must be greater than 0");
        }
    }
}