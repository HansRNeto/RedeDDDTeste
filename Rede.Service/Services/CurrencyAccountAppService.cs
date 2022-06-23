using System.Text.RegularExpressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Rede.Domain.Commands.CurrencyAccount;
using Rede.Domain.Core.Bus;
using Rede.Domain.Core.Notifications;
using Rede.Domain.Interfaces;
using Rede.Domain.Specifications;
using Rede.Service.EventSourcedNormalizers.CurrencyAccount;
using Rede.Service.Interfaces;
using Rede.Service.ViewModels;

namespace Rede.Service.Services;

public class CurrencyAccountAppService : ICurrencyAccountAppService
    {
        private readonly IMapper _mapper;
        private readonly ICurrencyAccountRepository _currencyAccountRepository;
        private readonly IMediatorHandler _bus;

        public CurrencyAccountAppService(IMapper mapper,
            ICurrencyAccountRepository currencyAccountRepository,
                                  IMediatorHandler bus)
        {
            _mapper = mapper;
            _currencyAccountRepository = currencyAccountRepository;
            _bus = bus;
        }

        public IEnumerable<CurrencyAccountViewModel> GetAll()
        {
            return _currencyAccountRepository.GetAll().ProjectTo<CurrencyAccountViewModel>(_mapper.ConfigurationProvider);
        }

        public IEnumerable<CurrencyAccountViewModel> GetAll(int skip, int take)
        {
            return _currencyAccountRepository.GetAll(new CurrencyAccountFilterPaginatedSpecification(skip, take))
                .ProjectTo<CurrencyAccountViewModel>(_mapper.ConfigurationProvider);
        }

        public CurrencyAccountViewModel GetById(Guid id)
        {
            return _mapper.Map<CurrencyAccountViewModel>(_currencyAccountRepository.GetById(id));
        }

        public CurrencyAccountViewModel GetByCustomer(Guid customerId)
        {
            return _mapper.Map<CurrencyAccountViewModel>(_currencyAccountRepository.GetByCustomerId(customerId));
        }

        public CurrencyAccountViewModel GetByAccountNumber(string accountNumber)
        {
            const string regExp = @"[^\w\d]";
            var cc = Regex.Replace(accountNumber, regExp, "");
            
            var ccNumber = int.Parse(cc[..6]);
            var ccDigit = int.Parse(cc[6..8]);
            
            var account = _mapper.Map<CurrencyAccountViewModel>(_currencyAccountRepository.GetByAccountNumber(ccNumber, ccDigit));

            if (account != null) return account;
            
            _bus.RaiseEvent(new DomainNotification("Currency Account not found", "The Currency Account do not exist."));
            // throw new FormatException("The Currency Account do not exist.");
            return new CurrencyAccountViewModel();
        }

        public void Register(CurrencyAccountViewModel customerViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewCurrencyAccountCommand>(customerViewModel);
            _bus.SendCommand(registerCommand);
        }

        public void Update(CurrencyAccountViewModel customerViewModel)
        {
            var updateCommand = _mapper.Map<UpdateCurrencyAccountCommand>(customerViewModel);
            _bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveCurrencyAccountCommand(id);
            _bus.SendCommand(removeCommand);
        }

        IList<CurrencyAccountHistoryData> ICurrencyAccountAppService.GetAllHistory(Guid id)
        {
            return null!;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
