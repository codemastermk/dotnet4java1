using Microsoft.EntityFrameworkCore;
using night_life_sk.Data;
using night_life_sk.Models;
using night_life_sk.Services;
using night_life_sk.Exceptions;
using night_life_sk.Services.persistence;
using night_life_sk.Dto.Place;

namespace night_life_sk.Repositories
{
    internal class PartyEventRepository
    {
        private readonly EntityPersistenceService entityPersistenceService;

        public PartyEventRepository(EntityPersistenceService entityPersistenceService)
        {
            this.entityPersistenceService = entityPersistenceService;
        }

        internal Task Add(PartyEvent partyEvent) => entityPersistenceService.Add(partyEvent);

        internal Task<List<PartyEvent>> FindAll() => entityPersistenceService.FindAll<PartyEvent>();

        internal Task<PartyEvent> FindById(int id) => entityPersistenceService.FindById<PartyEvent>(id);

        internal Task Delete(int id) => entityPersistenceService.Delete<PartyEvent>(id);

        internal Task Update(PartyEvent partyEvent) => entityPersistenceService.Update(partyEvent);

        internal Task<List<PartyEvent>> FindAllEventsByDate(DateTime date) =>
            entityPersistenceService.FindAllEventsByDate(date);

        internal Task<List<PartyEvent>> FindAllFilteredEvents(FilteredEventsDto filteredEvents) =>
           entityPersistenceService.FindAllFilteredEvents(filteredEvents);
    }
}
