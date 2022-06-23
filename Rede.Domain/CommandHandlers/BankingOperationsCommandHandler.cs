using MediatR;
using Rede.Domain.Commands.BankingOperations;
using Rede.Domain.Core.Bus;
using Rede.Domain.Core.Notifications;
using Rede.Domain.Events.BankingOperations;
using Rede.Domain.Interfaces;
using Rede.Domain.Models;

namespace Rede.Domain.CommandHandlers;

    public class BankingOperationsCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewBankingOperationsCommand, bool>,
        IRequestHandler<UpdateBankingOperationsCommand, bool>,
        IRequestHandler<RemoveBankingOperationsCommand, bool>
    {
        private readonly IBankingOperationsRepository _bankingOperationsRepository;
        private readonly IMediatorHandler _bus;

        public BankingOperationsCommandHandler(IBankingOperationsRepository bankingOperationsRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _bankingOperationsRepository = bankingOperationsRepository;
            _bus = bus;
        }

        public Task<bool> Handle(RegisterNewBankingOperationsCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }   

            var bankingOperations = new BankingOperations(Guid.NewGuid(), message.OriginAccount, message.DestinationAccount, message.Amount, message.Operation);

            _bankingOperationsRepository.Add(bankingOperations);

            if (Commit())
            {
                _bus.RaiseEvent(new BankingOperationsRegisteredEvent(bankingOperations.Id, bankingOperations.OriginAccount, bankingOperations.DestinatioAccount, bankingOperations.Amount, bankingOperations.Operation));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateBankingOperationsCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            if (Commit())
            {
                _bus.RaiseEvent(new BankingOperationsUpdatedEvent(message.Id, message.OriginAccount, message.DestinationAccount, message.Amount, message.Operation));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveBankingOperationsCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            _bankingOperationsRepository.Remove(message.Id);

            if (Commit())
            {
                _bus.RaiseEvent(new BankingOperationsRemovedEvent(message.Id));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _bankingOperationsRepository.Dispose();
        }
    }
