using System.Text.Json;
using Rede.Domain.Core.Events;
using Rede.Domain.Interfaces;
using Rede.Infra.Data.Repository.EventSourcing;

namespace Rede.Infra.Data.EventSourcing;

public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IUser _user;

        public SqlEventStore(IEventStoreRepository eventStoreRepository, IUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }

        public void Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonSerializer.Serialize(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                "System");

            _eventStoreRepository.Store(storedEvent);
        }
    }
