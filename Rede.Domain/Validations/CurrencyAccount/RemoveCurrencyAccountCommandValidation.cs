using Rede.Domain.Commands;
using Rede.Domain.Commands.CurrencyAccount;

namespace Rede.Domain.Validations.CurrencyAccount;

public class RemoveCurrencyAccountCommandValidation : CurrencyAccountValidation<RemoveCurrencyAccountCommand>
    {
        public RemoveCurrencyAccountCommandValidation()
        {
            ValidateId();
        }
    }
