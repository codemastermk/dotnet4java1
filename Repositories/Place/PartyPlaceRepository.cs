﻿using night_life_sk.Models;
using night_life_sk.Services.persistence;

namespace night_life_sk.Repositories.place
{
    public class PartyPlaceRepository : IPartyPlaceRepository
    {
        private readonly EntityPersistenceService entityPersistenceService;

        public PartyPlaceRepository(EntityPersistenceService entityPersistenceService)
        {
            this.entityPersistenceService = entityPersistenceService;
        }

        public void Add(PartyPlace partyPlace) => entityPersistenceService.Add(partyPlace);

        public void Delete(int id) => entityPersistenceService.Delete<PartyPlace>(id);

        public HashSet<PartyPlace> FindAll() => entityPersistenceService.FindAll<PartyPlace>();

        public HashSet<PartyEvent> FindAllEventsByDate(DateTime date) =>
            entityPersistenceService.FindAllEventsByDate(date);
        public PartyPlace FindById(int id) => entityPersistenceService.FindById<PartyPlace>(id);

        public void Update(PartyPlace partyPlace) => entityPersistenceService.Update(partyPlace);

        PartyPlace IPartyPlaceRepository.FindByXYTime(double longitude, double latitude, DateTime date) =>
            entityPersistenceService.FindByXYTime(longitude, latitude, date);
    }
}
