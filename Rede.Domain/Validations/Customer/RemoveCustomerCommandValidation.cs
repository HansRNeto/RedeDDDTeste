using Rede.Domain.Commands.Customer;

namespace Rede.Domain.Validations.Customer;

public class RemoveCustomerCommandValidation : CustomerValidation<RemoveCustomerCommand>
    {
        public RemoveCustomerCommandValidation()
        {
            ValidateId();
        }
    }
