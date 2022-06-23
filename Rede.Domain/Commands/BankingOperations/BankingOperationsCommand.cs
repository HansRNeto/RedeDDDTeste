using Rede.Domain.Core.Command;

namespace Rede.Domain.Commands.BankingOperations;

public abstract class BankingOperationsCommand : Command
    {
        public Guid Id { get; protected set; }

        public string OriginAccount { get; protected set; }

        public string DestinationAccount { get; protected set; }
        
        public double Amount { get; protected set; }

        public string Operation { get; protected set; }
    }
