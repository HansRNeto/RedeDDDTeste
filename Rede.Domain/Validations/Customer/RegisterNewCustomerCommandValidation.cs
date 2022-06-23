using Rede.Domain.Commands.Customer;

namespace Rede.Domain.Validations.Customer;

public class RegisterNewCustomerCommandValidation : CustomerValidation<RegisterNewCustomerCommand>
    {
        public RegisterNewCustomerCommandValidation()
        {
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
            ValidateDocument();
        }
    }
