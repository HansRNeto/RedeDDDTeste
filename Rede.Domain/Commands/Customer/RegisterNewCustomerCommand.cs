using Rede.Domain.Validations;
using Rede.Domain.Validations.Customer;

namespace Rede.Domain.Commands.Customer;

public class RegisterNewCustomerCommand : CustomerCommand
    {
        public RegisterNewCustomerCommand(string name, string email, DateTime birthDate, string document)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Document = document;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
