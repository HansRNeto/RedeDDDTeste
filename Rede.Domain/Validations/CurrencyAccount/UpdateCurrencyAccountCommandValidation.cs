using Rede.Domain.Commands;
using Rede.Domain.Commands.CurrencyAccount;

namespace Rede.Domain.Validations.CurrencyAccount;

public class UpdateCurrencyAccountCommandValidation : CurrencyAccountValidation<UpdateCurrencyAccountCommand>
    {
        public UpdateCurrencyAccountCommandValidation()
        {
            ValidateId();
            ValidateCustomerId();
            ValidateBalance();
            ValidateDigit();
            ValidateNumberAccount();
        }
    }
