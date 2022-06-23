using Rede.Domain.Core.Events;

namespace Rede.Domain.Events.CurrencyAccount;

public class CurrencyAccountUpdatedEvent : Event
    {
        public CurrencyAccountUpdatedEvent(Guid id, int numberAccount, int digit, double balance, Guid customerId)
        {
            Id = id;
            NumberAccount = numberAccount;
            Digit = digit;
            Balance = balance;
            AggregateId = id;
            CustomerId = customerId;
        }
        public Guid Id { get; set; }

        public int NumberAccount { get; private set; }

        public int Digit { get; private set; }
        
        public double Balance { get; private set; }

        public Guid CustomerId { get; private set; }
    }

