using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rede.Domain.Core.Bus;
using Rede.Domain.Core.Notifications;
using Rede.Service.Interfaces;
using Rede.Service.ViewModels;

namespace Rede.Application.Controllers;

[Authorize]
    public class CustomerController : ApiController
    {
        private readonly ICustomerAppService _customerAppService;
        private readonly ICurrencyAccountAppService _currencyAccountAppService;

        public CustomerController(
            ICustomerAppService customerAppService,
            ICurrencyAccountAppService currencyAccountAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _customerAppService = customerAppService;
            _currencyAccountAppService = currencyAccountAppService;
        }

        [HttpGet]
        [Authorize]
        [Route("all")]
        public IActionResult Get()
        {
            return Response(200,_customerAppService.GetAll());
        }

        [HttpGet]
        [Authorize]
        [Route("{document}")]
        public IActionResult Get(string document)
        {
            var customerViewModel = _customerAppService.GetByDocument(document);

            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            return customerViewModel == null ? Response(404) : Response(200, new
            {
                customerViewModel.Id,
                customerViewModel.Name,
                customerViewModel.Document,
                customerViewModel.Email,
                customerViewModel.CurrencyAccount.NumberAccount,
                customerViewModel.CurrencyAccount.Digit
            });
        }

        [HttpPost]
        [Authorize]
        [Route("create")]
        public IActionResult Post([FromBody]CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(400, customerViewModel);
            }

            _customerAppService.Register(customerViewModel);
            
            var customer = _customerAppService.GetByDocument(customerViewModel.Document);
            
            _currencyAccountAppService.Register(new CurrencyAccountViewModel()
            {
                CustomerId = customer.Id
            });

            var currencyAccount = _currencyAccountAppService.GetByCustomer(customer.Id);

            var responseData = new
            {
                customer.Name,
                customer.Email,
                customer.Document,
                currencyAccount.NumberAccount,
                currencyAccount.Digit,
                currencyAccount.Balance
            };

            return Response(201,responseData);
        }

        [HttpPut]
        [Authorize]
        [Route("update")]
        public IActionResult Put([FromBody]CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(400, customerViewModel);
            }

            _customerAppService.Update(customerViewModel);

            return Response(204, customerViewModel);
        }

        [HttpDelete]
        [Authorize]
        [Route("delete")]
        public IActionResult Delete(Guid id)
        {
            _customerAppService.Remove(id);

            return Response();
        }

        [HttpGet]
        [Authorize]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var customerHistoryData = _customerAppService.GetAllHistory(id);
            return Response(200, customerHistoryData);
        }

        [HttpGet]
        [Authorize]
        [Route("pagination")]
        public IActionResult Pagination(int skip, int take)
        {
            return Response(200,_customerAppService.GetAll(skip, take));
        }
    }
