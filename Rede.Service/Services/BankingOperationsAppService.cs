using AutoMapper;
using AutoMapper.QueryableExtensions;
using Rede.Domain.Commands.BankingOperations;
using Rede.Domain.Commands.CurrencyAccount;
using Rede.Domain.Core.Bus;
using Rede.Domain.Interfaces;
using Rede.Domain.Specifications;
using Rede.Service.EventSourcedNormalizers.BankingOperations;
using Rede.Service.Interfaces;
using Rede.Service.ViewModels;

namespace Rede.Service.Services;

public class BankingOperationsAppService : IBankingOperationsAppService
    {
        private readonly IMapper _mapper;
        private readonly IBankingOperationsRepository _bankingOperationsRepository;
        private readonly IMediatorHandler _bus;

        public BankingOperationsAppService(IMapper mapper,
            IBankingOperationsRepository bankingOperationsRepository,
                                  IMediatorHandler bus)
        {
            _mapper = mapper;
            _bankingOperationsRepository = bankingOperationsRepository;
            _bus = bus;
        }

        public IEnumerable<BankingOperationsViewModel> GetAll()
        {
            return _bankingOperationsRepository.GetAll().ProjectTo<BankingOperationsViewModel>(_mapper.ConfigurationProvider);
        }

        public IEnumerable<BankingOperationsViewModel> GetAll(int skip, int take)
        {
            return _bankingOperationsRepository.GetAll(new BankingOperationsFilterPaginatedSpecification(skip, take))
                .ProjectTo<BankingOperationsViewModel>(_mapper.ConfigurationProvider);
        }

        public BankingOperationsViewModel GetById(Guid id)
        {
            return _mapper.Map<BankingOperationsViewModel>(_bankingOperationsRepository.GetById(id));
        }

        public void Register(BankingOperationsViewModel customerViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewBankingOperationsCommand>(customerViewModel);
            _bus.SendCommand(registerCommand);
        }

        public void Update(BankingOperationsViewModel customerViewModel)
        {
            var updateCommand = _mapper.Map<UpdateBankingOperationsCommand>(customerViewModel);
            _bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveBankingOperationsCommand(id);
            _bus.SendCommand(removeCommand);
        }

        IList<BankingOperationsHistoryData> IBankingOperationsAppService.GetAllHistory(Guid id)
        {
            return null!;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
