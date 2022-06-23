using FluentValidation.Results;
using Rede.Domain.Core.Events;

namespace Rede.Domain.Core.Command;

    public abstract class Command : Message
    {
        public DateTime Timestamp { get; private set; }
        
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
