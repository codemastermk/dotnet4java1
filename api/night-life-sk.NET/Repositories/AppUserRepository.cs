using night_life_sk.Models;
using night_life_sk.Services.persistence;

namespace night_life_sk.Repositories
{
    internal class AppUserRepository
    {
        private readonly EntityPersistenceService entityPersistenceService;
        public AppUserRepository(EntityPersistenceService entityPersistenceService)
        {
            this.entityPersistenceService = entityPersistenceService;
        }

        internal Task Add(AppUser appUser) => entityPersistenceService.Add(appUser);

        internal Task<List<AppUser>> FindAll() => entityPersistenceService.FindAll<AppUser>();

        internal Task<AppUser> FindById(int id) => entityPersistenceService.FindById<AppUser>(id);

        internal Task Update(AppUser appUser) => entityPersistenceService.Update(appUser);

        internal void Delete(int id) => entityPersistenceService.Delete<AppUser>(id);

        internal Task<HashSet<AppUser>> FindAllByPartyName(string eventName) => 
            entityPersistenceService.FindGuestByPartyName(eventName);

    }
}
