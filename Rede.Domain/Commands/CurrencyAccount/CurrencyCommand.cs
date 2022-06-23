using Rede.Domain.Core.Command;

namespace Rede.Domain.Commands.CurrencyAccount;

public abstract class CurrencyAccountCommand : Command
    {
        public Guid Id { get; protected set; }

        public int NumberAccount { get; protected set; }

        public int Digit { get; protected set; }
        
        public double Balance { get; protected set; }

        public Guid CustomerId { get; protected set; }
    }
