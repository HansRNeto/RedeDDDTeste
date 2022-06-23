using Rede.Domain.Core.Events;

namespace Rede.Infra.Data.Repository.EventSourcing;

public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
