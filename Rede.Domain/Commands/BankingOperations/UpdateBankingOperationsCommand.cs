using Rede.Domain.Commands.CurrencyAccount;
using Rede.Domain.Validations.BankingOperations;
using Rede.Domain.Validations.CurrencyAccount;

namespace Rede.Domain.Commands.BankingOperations;

    public class UpdateBankingOperationsCommand : BankingOperationsCommand
    {
        public UpdateBankingOperationsCommand(Guid id, string originAccount, string destinationAccount, double amount, string operation)
        {
            Id = id;
            OriginAccount = originAccount;
            DestinationAccount = destinationAccount;
            Amount = amount;
            Operation = operation;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateBankingOperationsCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
