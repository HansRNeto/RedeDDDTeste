using Rede.Domain.Core.Events;

namespace Rede.Domain.Core.Notifications;

    public interface IDomainNotificationHandler<T> : IHandler<T> where T : Message
    {
        bool HasNotifications();
        List<T> GetNotifications();
    }
