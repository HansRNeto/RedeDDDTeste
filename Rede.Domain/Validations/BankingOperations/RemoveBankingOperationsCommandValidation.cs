using Rede.Domain.Commands.BankingOperations;

namespace Rede.Domain.Validations.BankingOperations;

public class RemoveBankingOperationsCommandValidation : BankingOperationsValidation<RemoveBankingOperationsCommand>
    {
        public RemoveBankingOperationsCommandValidation()
        {
            ValidateId();
        }
    }
