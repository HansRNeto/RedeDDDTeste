using AutoMapper;
using Rede.Domain.Commands;
using Rede.Domain.Commands.BankingOperations;
using Rede.Domain.Commands.CurrencyAccount;
using Rede.Domain.Commands.Customer;
using Rede.Service.ViewModels;

namespace Rede.Service.AutoMapper;

public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            #region Customer
            CreateMap<CustomerViewModel, RegisterNewCustomerCommand>()
                .ConstructUsing(c => new RegisterNewCustomerCommand(c.Name, c.Email, c.BirthDate, c.Document));
            CreateMap<CustomerViewModel, UpdateCustomerCommand>()
                .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.Name, c.Email, c.BirthDate, c.Document));
            #endregion
            
            #region CurrencyAccount
            CreateMap<CurrencyAccountViewModel, RegisterNewCurrencyAccountCommand>()
                .ConstructUsing(c => new RegisterNewCurrencyAccountCommand(c.NumberAccount, c.Digit, c.Balance, c.CustomerId));
            CreateMap<CurrencyAccountViewModel, UpdateCurrencyAccountCommand>()
                .ConstructUsing(c => new UpdateCurrencyAccountCommand(c.Id, c.NumberAccount, c.Digit, c.Balance, c.CustomerId));
            #endregion
            
            #region BankingOperations
            CreateMap<BankingOperationsViewModel, RegisterNewBankingOperationsCommand>()
                .ConstructUsing(c => new RegisterNewBankingOperationsCommand(c.OriginAccount, c.DestinationAccount, c.Amount, c.Operation));
            CreateMap<BankingOperationsViewModel, UpdateBankingOperationsCommand>()
                .ConstructUsing(c => new UpdateBankingOperationsCommand(c.Id, c.OriginAccount, c.DestinationAccount, c.Amount, c.Operation));
            #endregion

        }
    }
