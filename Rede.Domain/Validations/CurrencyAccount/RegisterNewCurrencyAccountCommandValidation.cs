using Rede.Domain.Commands;
using Rede.Domain.Commands.CurrencyAccount;

namespace Rede.Domain.Validations.CurrencyAccount;

public class RegisterNewCurrencyAccountCommandValidation : CurrencyAccountValidation<RegisterNewCurrencyAccountCommand>
    {
        public RegisterNewCurrencyAccountCommandValidation()
        {
            // ValidateId();
            // ValidateNumberAccount();
            // ValidateBalance();
            // ValidateDigit();
            ValidateCustomerId();
        }
    }
