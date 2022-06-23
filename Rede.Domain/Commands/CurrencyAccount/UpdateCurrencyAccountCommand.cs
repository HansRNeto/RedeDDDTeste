using Rede.Domain.Validations;
using Rede.Domain.Validations.CurrencyAccount;

namespace Rede.Domain.Commands.CurrencyAccount;

    public class UpdateCurrencyAccountCommand : CurrencyAccountCommand
    {
        public UpdateCurrencyAccountCommand(Guid id, int numberAccount, int digit, double balance, Guid customerId)
        {
            Id = id;
            NumberAccount = numberAccount;
            Digit = digit;
            Balance = balance;
            CustomerId = customerId;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCurrencyAccountCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
