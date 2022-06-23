using Rede.Domain.Validations;
using Rede.Domain.Validations.CurrencyAccount;

namespace Rede.Domain.Commands.CurrencyAccount;

public class RemoveCurrencyAccountCommand : CurrencyAccountCommand
    {
        public RemoveCurrencyAccountCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveCurrencyAccountCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

