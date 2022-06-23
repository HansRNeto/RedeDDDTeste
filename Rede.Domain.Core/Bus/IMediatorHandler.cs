using Rede.Domain.Core.Events;

namespace Rede.Domain.Core.Bus;

    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command.Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }

