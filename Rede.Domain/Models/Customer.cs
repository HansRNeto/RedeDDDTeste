using Rede.Domain.Core.Models;

namespace Rede.Domain.Models;

public class Customer : EntityAudit
    {
        public Customer(Guid id, string name, string email, DateTime birthDate, string document)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Document = document;
        }

        // Empty constructor for EF
        protected Customer() { }

        public string Name { get; private set; }

        public string Email { get; private set; }
        
        public string Document { get; private set; }

        public DateTime BirthDate { get; private set; }

        //EF Navigation
        public CurrencyAccount CurrencyAccount { get; set; }
    }
