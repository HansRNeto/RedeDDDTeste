using FluentValidation;
using Rede.Domain.Commands.BankingOperations;

namespace Rede.Domain.Validations.BankingOperations;

    public abstract class BankingOperationsValidation<T> : AbstractValidator<T> where T : BankingOperationsCommand
    {
        protected void ValidateOriginAccount()
        {
            RuleFor(c => c.OriginAccount)
                .NotEmpty().WithMessage("Please ensure you have entered the OriginAccount");
        }

        protected void ValidateDestinationAccount()
        {
            RuleFor(c => c.DestinationAccount)
                .NotEmpty()
                .NotEmpty().WithMessage("Please ensure you have entered the DestinationAccount");
        }

        protected void ValidateAmount()
        {
            RuleFor(c => c.Amount).NotNull().WithMessage("The Balance must have a value");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
        
        protected void ValidateOperation()
        {
            RuleFor(c => c.Operation)
                .NotEmpty().WithMessage("Please ensure you have entered the Operation");
        }
    }
