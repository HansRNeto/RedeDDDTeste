using Rede.Domain.Commands.CurrencyAccount;
using Rede.Domain.Validations.BankingOperations;
using Rede.Domain.Validations.CurrencyAccount;

namespace Rede.Domain.Commands.BankingOperations;

public class RemoveBankingOperationsCommand : BankingOperationsCommand
    {
        public RemoveBankingOperationsCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveBankingOperationsCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

