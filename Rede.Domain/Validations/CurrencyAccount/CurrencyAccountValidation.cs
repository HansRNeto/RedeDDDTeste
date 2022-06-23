using FluentValidation;
using Rede.Domain.Commands.CurrencyAccount;

namespace Rede.Domain.Validations.CurrencyAccount;

    public abstract class CurrencyAccountValidation<T> : AbstractValidator<T> where T : CurrencyAccountCommand
    {
        protected void ValidateNumberAccount()
        {
            RuleFor(c => c.NumberAccount)
                .NotEmpty().WithMessage("Please ensure you have entered the NumberAccount");
        }

        protected void ValidateDigit()
        {
            RuleFor(c => c.Digit)
                .NotEmpty()
                .WithMessage("The Digit must have a value");
        }

        protected void ValidateBalance()
        {
            RuleFor(c => c.Balance).NotNull().WithMessage("The Balance must have a value");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
        
        protected void ValidateCustomerId()
        {
            RuleFor(c => c.CustomerId)
                .NotEmpty().WithMessage("Please ensure you have entered the CustomerId")
                .NotNull().WithMessage("Please ensure you have entered the CustomerId");
        }
    }
