using AutoMapper;
using AutoMapper.QueryableExtensions;
using Rede.Domain.Commands;
using Rede.Domain.Commands.Customer;
using Rede.Domain.Core.Bus;
using Rede.Domain.Interfaces;
using Rede.Domain.Specifications;
using Rede.Service.EventSourcedNormalizers;
using Rede.Service.EventSourcedNormalizers.Customer;
using Rede.Service.Interfaces;
using Rede.Service.ViewModels;

namespace Rede.Service.Services;

public class CustomerAppService : ICustomerAppService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediatorHandler _bus;

        public CustomerAppService(IMapper mapper,
                                  ICustomerRepository customerRepository,
                                  IMediatorHandler bus)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _bus = bus;
        }

        public IEnumerable<CustomerViewModel> GetAll()
        {
            return _customerRepository.GetAll().ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider);
        }

        public IEnumerable<CustomerViewModel> GetAll(int skip, int take)
        {
            return _customerRepository.GetAll(new CustomerFilterPaginatedSpecification(skip, take))
                .ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider);
        }

        public CustomerViewModel GetById(Guid id)
        {
            return _mapper.Map<CustomerViewModel>(_customerRepository.GetById(id));
        }

        public CustomerViewModel GetByDocument(string document)
        {
            return _mapper.Map<CustomerViewModel>(_customerRepository.GetByDocument(document));
        }

        public void Register(CustomerViewModel customerViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewCustomerCommand>(customerViewModel);
            _bus.SendCommand(registerCommand);
        }

        public void Update(CustomerViewModel customerViewModel)
        {
            var updateCommand = _mapper.Map<UpdateCustomerCommand>(customerViewModel);
            _bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveCustomerCommand(id);
            _bus.SendCommand(removeCommand);
        }

        public IList<CustomerHistoryData> GetAllHistory(Guid id)
        {
            return null!;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
