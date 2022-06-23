using MediatR;
using Rede.Domain.Commands;
using Rede.Domain.Commands.CurrencyAccount;
using Rede.Domain.Core.Bus;
using Rede.Domain.Core.Notifications;
using Rede.Domain.Events;
using Rede.Domain.Events.CurrencyAccount;
using Rede.Domain.Interfaces;
using Rede.Domain.Models;

namespace Rede.Domain.CommandHandlers;

    public class CurrencyAccountCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewCurrencyAccountCommand, bool>,
        IRequestHandler<UpdateCurrencyAccountCommand, bool>,
        IRequestHandler<RemoveCurrencyAccountCommand, bool>
    {
        private readonly ICurrencyAccountRepository _currencyAccountRepository;
        private readonly IMediatorHandler Bus;

        public CurrencyAccountCommandHandler(ICurrencyAccountRepository currencyAccountRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _currencyAccountRepository = currencyAccountRepository;
            Bus = bus;
        }

        public Task<bool> Handle(RegisterNewCurrencyAccountCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }   

            var currencyAccount = new CurrencyAccount(Guid.NewGuid(),  int.Parse(new Random().Next(0, 1000000).ToString("D6")), int.Parse(new Random().Next(0, 99).ToString("D6")), 0, message.CustomerId);

            if (_currencyAccountRepository.GetByCustomerId(currencyAccount.CustomerId) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The customer e-mail has already been taken."));
                return Task.FromResult(false);
            }

            _currencyAccountRepository.Add(currencyAccount);

            if (Commit())
            {
                Bus.RaiseEvent(new CurrencyAccountRegisteredEvent(currencyAccount.Id, currencyAccount.NumberAccount, currencyAccount.Digit, currencyAccount.Balance, currencyAccount.CustomerId));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateCurrencyAccountCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var currencyAccount = new CurrencyAccount(message.Id, message.NumberAccount, message.Digit, message.Balance, message.CustomerId);
            var existingCurrencyAccount = _currencyAccountRepository.GetByCustomerId(currencyAccount.CustomerId);

            if (existingCurrencyAccount != null && existingCurrencyAccount.Id != currencyAccount.Id)
            {
                if (!existingCurrencyAccount.Equals(currencyAccount))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, "The currency account has already been taken."));
                    return Task.FromResult(false);
                }
            }

            _currencyAccountRepository.Update(currencyAccount);

            if (Commit())
            {
                Bus.RaiseEvent(new CurrencyAccountUpdatedEvent(currencyAccount.Id, currencyAccount.NumberAccount, currencyAccount.Digit, currencyAccount.Balance, currencyAccount.CustomerId));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveCurrencyAccountCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            _currencyAccountRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new CurrencyAccountRemovedEvent(message.Id));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _currencyAccountRepository.Dispose();
        }
    }
