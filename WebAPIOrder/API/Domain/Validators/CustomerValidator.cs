using API.Domain.Models;
using FluentValidation;

namespace API.Domain.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerModel>
    {
        public CustomerValidator()
        {
            RuleFor(p => p.Password)
                .NotEmpty()
                    .WithMessage("password not be null or empty")
                .MinimumLength(8)
                    .WithMessage("password less than 8 characters");

            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("Full name must not be null or empty");
        }
    }
}