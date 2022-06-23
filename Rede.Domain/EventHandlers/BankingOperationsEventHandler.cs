using MediatR;
using Rede.Domain.Events.BankingOperations;

namespace Rede.Domain.EventHandlers;

public class BankingOperationsEventHandler :
    INotificationHandler<BankingOperationsRegisteredEvent>,
    INotificationHandler<BankingOperationsUpdatedEvent>,
    INotificationHandler<BankingOperationsRemovedEvent>
{
    public Task Handle(BankingOperationsUpdatedEvent message, CancellationToken cancellationToken)
    {
        // Send some notification e-mail

        return Task.CompletedTask;
    }

    public Task Handle(BankingOperationsRegisteredEvent message, CancellationToken cancellationToken)
    {
        // Send some greetings e-mail

        return Task.CompletedTask;
    }

    public Task Handle(BankingOperationsRemovedEvent message, CancellationToken cancellationToken)
    {
        // Send some see you soon e-mail

        return Task.CompletedTask;
    }
}