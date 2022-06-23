using Rede.Domain.Core.Events;

namespace Rede.Domain.Events.BankingOperations;

public class BankingOperationsRemovedEvent : Event
    {
        public BankingOperationsRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
