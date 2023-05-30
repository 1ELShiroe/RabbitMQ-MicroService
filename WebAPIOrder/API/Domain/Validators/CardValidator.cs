using API.Domain.Models;
using FluentValidation;

namespace API.Domain.Validators
{
    public class CardValidator : AbstractValidator<CardModel>
    {
        public CardValidator()
        {
            RuleFor(x => x.NameCard)
                .NotEmpty()
                    .WithMessage("Full name must not be null or empty")
                    .MinimumLength(5);

            RuleFor(x => x.CVV)
                .NotEmpty()
                    .WithMessage("the Security Code (CVV) must not be null or empty")
                .MaximumLength(4)
                    .WithMessage("the Security Code (CVV) cannot be greater than 4")
                .MinimumLength(3)
                    .WithMessage("the Security Code (CVV) cannot be less than 3");

            RuleFor(x => x.Expiration)
               .NotEmpty()
                    .WithMessage("expiration must not be null or empty")
                    .Length(5)
                    .WithMessage("expiration cannot be less than or greater than 5");

            /*
                Visa e Mastercard: 16 dígitos
                American Express: 15 dígitos
                Diners Club: 14 dígitos
                Discover: 16 dígitos
                JCB: 16 dígitos
            */
            RuleFor(x => x.NumberCard)
                .NotEmpty()
                    .WithMessage("card numbers must not be null or empty")
                .MaximumLength(16)
                    .WithMessage("card number with more than 16 numbers")
                .MinimumLength(14)
                    .WithMessage("card number with less than 14 numbers");
        }
    }
}