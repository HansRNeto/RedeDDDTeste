using Rede.Domain.Commands.CurrencyAccount;
using Rede.Domain.Validations.BankingOperations;
using Rede.Domain.Validations.CurrencyAccount;

namespace Rede.Domain.Commands.BankingOperations;

public class RegisterNewBankingOperationsCommand : BankingOperationsCommand
    {
        public RegisterNewBankingOperationsCommand(string originAccount, string destinationAccount, double amount, string operation)
        {
            OriginAccount = originAccount;
            DestinationAccount = destinationAccount;
            Amount = amount;
            Operation = operation;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewBankingOperationsCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
