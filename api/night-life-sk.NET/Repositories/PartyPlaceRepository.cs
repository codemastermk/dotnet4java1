using night_life_sk.Models;
using night_life_sk.Services.persistence;

namespace night_life_sk.Repositories
{
    internal class PartyPlaceRepository
    {
        private readonly EntityPersistenceService entityPersistenceService;

        public PartyPlaceRepository(EntityPersistenceService entityPersistenceService)
        {
            this.entityPersistenceService = entityPersistenceService;
        }

        internal Task Add(PartyPlace partyPlace) => entityPersistenceService.Add(partyPlace);

        internal Task Delete(int id) => entityPersistenceService.Delete<PartyPlace>(id);

        internal Task<List<PartyPlace>> FindAll() => entityPersistenceService.FindAll<PartyPlace>();

        internal Task<PartyPlace> FindById(int id) => entityPersistenceService.FindById<PartyPlace>(id);

        internal Task Update(PartyPlace partyPlace) => entityPersistenceService.Update(partyPlace);

        internal Task<PartyPlace> FindByXYTime(double longitude, double latitude, DateTime date) =>
            entityPersistenceService.FindByXYTime(longitude, latitude, date);
    }
}
