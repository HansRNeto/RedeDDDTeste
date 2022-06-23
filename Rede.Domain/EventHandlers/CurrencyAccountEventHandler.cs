using MediatR;
using Rede.Domain.Events.CurrencyAccount;

namespace Rede.Domain.EventHandlers;

public class CurrencyAccountEventHandler :
    INotificationHandler<CurrencyAccountRegisteredEvent>,
    INotificationHandler<CurrencyAccountUpdatedEvent>,
    INotificationHandler<CurrencyAccountRemovedEvent>
{
    public Task Handle(CurrencyAccountUpdatedEvent message, CancellationToken cancellationToken)
    {
        // Send some notification e-mail

        return Task.CompletedTask;
    }

    public Task Handle(CurrencyAccountRegisteredEvent message, CancellationToken cancellationToken)
    {
        // Send some greetings e-mail

        return Task.CompletedTask;
    }

    public Task Handle(CurrencyAccountRemovedEvent message, CancellationToken cancellationToken)
    {
        // Send some see you soon e-mail

        return Task.CompletedTask;
    }
}