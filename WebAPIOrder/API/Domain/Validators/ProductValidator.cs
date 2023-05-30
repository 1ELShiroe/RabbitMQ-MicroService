using API.Domain.Models;
using FluentValidation;

namespace API.Domain.Validators
{
    public class ProductValidator : AbstractValidator<ProductModel>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Quantity)
                .NotEmpty()
                .GreaterThan(0)
                    .WithMessage("product quantity must be greater than 0");

            RuleFor(p => p.Value)
                .NotEmpty()
                .GreaterThan(0)
                    .WithMessage("product value needs to be greater than 0");

            RuleFor(p => p.Name)
                .NotEmpty()
                    .WithMessage("product name not informed");
        }
    }
}