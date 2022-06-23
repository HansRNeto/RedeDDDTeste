using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rede.Domain.Core.Bus;
using Rede.Domain.Core.Notifications;
using Rede.Service.Interfaces;
using Rede.Service.ViewModels;

namespace Rede.Application.Controllers;

[Authorize]
    public class BankingOperationsController : ApiController
    {

        private readonly IBankingOperationsAppService _bankingOperationsAppService;
        private readonly ICurrencyAccountAppService _currencyAccountAppService;
        
        public BankingOperationsController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator,
            ICurrencyAccountAppService currencyAccountAppService,
            IBankingOperationsAppService bankingOperationsAppService) : base(notifications, mediator)
        {
            _bankingOperationsAppService = bankingOperationsAppService;
            _currencyAccountAppService = currencyAccountAppService;
        }
        
        [HttpGet]
        [Authorize]
        [Route("all")]
        public IActionResult Get()
        {
            return Response(200, _bankingOperationsAppService.GetAll());
        }
        
        [HttpGet]
        [Authorize]
        [Route("balance")]
        public IActionResult Balance(string accountNumber)
        {
            var cAccountOrigin = _currencyAccountAppService.GetByAccountNumber(accountNumber);

            return cAccountOrigin == null ? Response(404) : Response(200, new
            {
                cAccountOrigin.NumberAccount,
                cAccountOrigin.Digit,
                cAccountOrigin.Balance
            });
        }
        
        [HttpPost]
        [Authorize]
        [Route("create")]
        public IActionResult Post([FromBody]BankingOperationsViewModel bankingOperationsViewModel)
        {
            if (bankingOperationsViewModel.Operation != "Transfer" &&
                bankingOperationsViewModel.Operation != "Deposit" &&
                bankingOperationsViewModel.Operation != "Withdraw") return BadRequest(new {statusCode = 400, error ="Types of Banking Operations is: Transfer, Deposit and Withdraw"});
            
            var cAccountOrigin = _currencyAccountAppService.GetByAccountNumber(bankingOperationsViewModel.OriginAccount);

            if (bankingOperationsViewModel.Operation != "Transfer")
            {
                var origin = OperationFlow(cAccountOrigin, bankingOperationsViewModel.Amount, bankingOperationsViewModel.Operation);
               
                return Response(201, new
                {
                    originAccount = origin.NumberAccount + "-" + origin.Digit,
                    destinationAccount = origin.NumberAccount + "-" + origin.Digit,
                    bankingOperationsViewModel.Amount,
                    bankingOperationsViewModel.Operation
                });
            }

            var cAccountDestination = _currencyAccountAppService.GetByAccountNumber(bankingOperationsViewModel.DestinationAccount);

            if (cAccountOrigin.NumberAccount == 0 || cAccountDestination.NumberAccount == 0) return Response(400);
            
            var destination = OperationFlow(cAccountDestination, bankingOperationsViewModel.Amount, bankingOperationsViewModel.Operation, false);
   
            var origination = OperationFlow(cAccountOrigin, bankingOperationsViewModel.Amount, bankingOperationsViewModel.Operation);

            return Response(200, new
            {
                originAccount = origination.NumberAccount + "-" + origination.Digit,
                destinationAccount = destination.NumberAccount + "-" + destination.Digit,
                bankingOperationsViewModel.Amount,
                bankingOperationsViewModel.Operation
            });

        }

        [HttpGet]
        [Authorize]
        [Route("pagination")]
        public IActionResult Pagination(int skip, int take)
        {
            return Response(200,_bankingOperationsAppService.GetAll(skip, take));
        }

        private CurrencyAccountViewModel OperationFlow(CurrencyAccountViewModel cAccount, double amount, string operation, bool origin = true)
        {
            _bankingOperationsAppService.Register(new BankingOperationsViewModel()
            {
                Operation = operation,
                DestinationAccount = cAccount.NumberAccount + "-" + cAccount.Digit,
                OriginAccount = cAccount.NumberAccount + "-" + cAccount.Digit,
                Amount = amount,
            });

            switch (operation)
            {
                case "Deposit": 
                    cAccount.Balance += amount;
                    break;
                case "Withdraw":
                    cAccount.Balance -= amount;
                    break;
                case "Transfer":
                    if(origin) cAccount.Balance -= amount;
                    else cAccount.Balance += amount;
                    break;
            }
            
            _currencyAccountAppService.Update(cAccount);
            return cAccount;
        }
    }
