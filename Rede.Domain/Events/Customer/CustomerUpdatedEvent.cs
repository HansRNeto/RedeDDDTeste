using Rede.Domain.Core.Events;

namespace Rede.Domain.Events.Customer;

public class CustomerUpdatedEvent : Event
    {
        public CustomerUpdatedEvent(Guid id, string name, string email, string document, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            AggregateId = id;
            Document = document;
        }
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }
        
        public string Document { get; private set; }

        public DateTime BirthDate { get; private set; }
    }

