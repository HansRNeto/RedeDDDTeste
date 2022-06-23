using AutoMapper;
using Rede.Domain.Models;
using Rede.Service.ViewModels;

namespace Rede.Service.AutoMapper; 
public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<CurrencyAccount, CurrencyAccountViewModel>();
            CreateMap<BankingOperations, BankingOperationsViewModel>();
        }
    }
