using Rede.Domain.Commands.BankingOperations;

namespace Rede.Domain.Validations.BankingOperations;

public class RegisterNewBankingOperationsCommandValidation : BankingOperationsValidation<RegisterNewBankingOperationsCommand>
    {
        public RegisterNewBankingOperationsCommandValidation()
        {
            // ValidateId();
            ValidateOriginAccount();
            ValidateDestinationAccount();
            ValidateOperation();
            ValidateAmount();
        }
    }
