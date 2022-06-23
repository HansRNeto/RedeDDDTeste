using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Rede.Domain.CommandHandlers;
using Rede.Domain.Commands;
using Rede.Domain.Commands.BankingOperations;
using Rede.Domain.Commands.CurrencyAccount;
using Rede.Domain.Commands.Customer;
using Rede.Domain.Core.Bus;
using Rede.Domain.Core.Events;
using Rede.Domain.Core.Notifications;
using Rede.Domain.EventHandlers;
using Rede.Domain.Events;
using Rede.Domain.Events.BankingOperations;
using Rede.Domain.Events.CurrencyAccount;
using Rede.Domain.Events.Customer;
using Rede.Domain.Interfaces;
using Rede.Domain.Services.Http;
using Rede.Infra.CrossCutting.Bus;
using Rede.Infra.CrossCutting.Identity.Authorization;
using Rede.Infra.CrossCutting.Identity.Models;
using Rede.Infra.CrossCutting.Identity.Services;
using Rede.Infra.Data.EventSourcing;
using Rede.Infra.Data.Repository;
using Rede.Infra.Data.Repository.EventSourcing;
using Rede.Infra.Data.UoW;
using Rede.Service.Interfaces;
using Rede.Service.Services;

namespace Rede.Infra.CrossCutting.IoC;

public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Services
            services.AddScoped<ICustomerAppService, CustomerAppService>();
            services.AddScoped<ICurrencyAccountAppService, CurrencyAccountAppService>();
            services.AddScoped<IBankingOperationsAppService, BankingOperationsAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            //TODO:Customer
            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerRemovedEvent>, CustomerEventHandler>();
            //TODO:CurrencyAccount
            services.AddScoped<INotificationHandler<CurrencyAccountRegisteredEvent>, CurrencyAccountEventHandler>();
            services.AddScoped<INotificationHandler<CurrencyAccountUpdatedEvent>, CurrencyAccountEventHandler>();
            services.AddScoped<INotificationHandler<CurrencyAccountRemovedEvent>, CurrencyAccountEventHandler>();
            //TODO:BankingOperations
            services.AddScoped<INotificationHandler<BankingOperationsRegisteredEvent>, BankingOperationsEventHandler>();
            services.AddScoped<INotificationHandler<BankingOperationsUpdatedEvent>, BankingOperationsEventHandler>();
            services.AddScoped<INotificationHandler<BankingOperationsRemovedEvent>, BankingOperationsEventHandler>();

            // Domain - Commands
            //TODO:Customer
            services.AddScoped<IRequestHandler<RegisterNewCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand, bool>, CustomerCommandHandler>();
            //TODO:CurrencyAccount
            services.AddScoped<IRequestHandler<RegisterNewCurrencyAccountCommand, bool>, CurrencyAccountCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCurrencyAccountCommand, bool>, CurrencyAccountCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCurrencyAccountCommand, bool>, CurrencyAccountCommandHandler>();
            //TODO:BankingOperations
            services.AddScoped<IRequestHandler<RegisterNewBankingOperationsCommand, bool>, BankingOperationsCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateBankingOperationsCommand, bool>, BankingOperationsCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveBankingOperationsCommand, bool>, BankingOperationsCommandHandler>();

            // Domain - 3rd parties
            services.AddScoped<IHttpService, HttpService>();
            // services.AddScoped<IMailService, MailService>();

            // Infra - Data
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICurrencyAccountRepository, CurrencyAccountRepository>();
            services.AddScoped<IBankingOperationsRepository, BankingOperationsRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();

            // Infra - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
            services.AddSingleton<IJwtFactory, JwtFactory>();
        }
    }
