using Rede.Domain.Core.Events;

namespace Rede.Domain.Events.BankingOperations;

public class BankingOperationsUpdatedEvent : Event
    {
        public BankingOperationsUpdatedEvent(Guid id, string originAccount, string destinationAccount, double amount, string operation)
        {
            Id = id;
            OriginAccount = originAccount;
            DestinationAccount = destinationAccount;
            Amout = amount;
            AggregateId = id;
            Operation = operation;
        }
        public Guid Id { get; set; }

        public string OriginAccount { get; private set; }

        public string DestinationAccount { get; private set; }
        
        public double Amout { get; private set; }

        public string Operation { get; private set; }
    }

