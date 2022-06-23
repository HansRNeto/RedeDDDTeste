using Rede.Domain.Commands.BankingOperations;

namespace Rede.Domain.Validations.BankingOperations;

public class UpdateBankingOperationsCommandValidation : BankingOperationsValidation<UpdateBankingOperationsCommand>
    {
        public UpdateBankingOperationsCommandValidation()
        {
            ValidateId();
            ValidateAmount();
            ValidateOperation();
            ValidateDestinationAccount();
            ValidateOriginAccount();
        }
    }
