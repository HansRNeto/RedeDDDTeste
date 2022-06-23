using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rede.Domain.Core.Bus;
using Rede.Domain.Core.Notifications;

namespace Rede.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Route("api/[controller]/[action]")]
public abstract class ApiController : ControllerBase
{
    private readonly DomainNotificationHandler _notifications;
    private readonly IMediatorHandler _mediator;

    protected ApiController(INotificationHandler<DomainNotification> notifications,
                            IMediatorHandler mediator)
    {
        _notifications = (DomainNotificationHandler)notifications;
        _mediator = mediator;
    }

    protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

    protected bool IsValidOperation()
    {
        return (!_notifications.HasNotifications());
    }

    protected new IActionResult Response(int statusCode = 200, object? data = null)
    {
        var message = _notifications.GetNotifications().Select(n => n.Value).FirstOrDefault();
        if (IsValidOperation())
        {
            return statusCode switch
            {
                404 => StatusCode(statusCode, new { statusCode, message = "Record not found." }),
                204 => StatusCode(statusCode, new { statusCode, data, message = "Updated Successfully" }),
                201 => Created("api", new { statusCode, data, message = "Created Successfully" }),
                200 => Ok(new { statusCode, message = "Success", data }),
                _ => Ok()
            };
        }

        return BadRequest(new
        {
            statusCode = 400,
            error = message
        });
    }

    protected void NotifyModelStateErrors()
    {
        var erros = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var erro in erros)
        {
            var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotifyError(string.Empty, erroMsg);
        }
    }

    protected void NotifyError(string code, string message)
    {
        _mediator.RaiseEvent(new DomainNotification(code, message));
    }

    protected void AddIdentityErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            NotifyError(result.ToString(), error.Description);
        }
    }
}
