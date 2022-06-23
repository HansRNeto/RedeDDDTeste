using Rede.Domain.Validations;
using Rede.Domain.Validations.CurrencyAccount;

namespace Rede.Domain.Commands.CurrencyAccount;

public class RegisterNewCurrencyAccountCommand : CurrencyAccountCommand
    {
        public RegisterNewCurrencyAccountCommand(int numberAccount, int digit, double balance, Guid customerId)
        {
            NumberAccount = numberAccount;
            Digit = digit;
            Balance = balance;
            CustomerId = customerId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewCurrencyAccountCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
