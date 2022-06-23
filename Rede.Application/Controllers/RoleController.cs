using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rede.Domain.Core.Bus;
using Rede.Domain.Core.Notifications;
using Rede.Infra.CrossCutting.Identity.Models.RoleViewModels;

namespace Rede.Application.Controllers;

public class RoleController : ApiController
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(400, model);
            }

            var role = new IdentityRole(model.Name);
            await _roleManager.CreateAsync(role);

            // Add RoleClaims
            // var roleClaim = new Claim("Customers", "Write");
            // await _roleManager.AddClaimAsync(role, roleClaim);

            return Response();
        }
    }
