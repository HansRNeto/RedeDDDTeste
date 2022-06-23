using Rede.Domain.Core.Events;

namespace Rede.Domain.Events.CurrencyAccount;

public class CurrencyAccountRemovedEvent : Event
    {
        public CurrencyAccountRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
