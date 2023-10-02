using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using night_life_sk.Data;
using night_life_sk.Dto.Place;
using night_life_sk.Exceptions;
using night_life_sk.Models;

namespace night_life_sk.Services.persistence
{

    internal class EntityPersistenceService
    {
        private readonly ILogger<EntityPersistenceService> logger;
        private readonly ScopedServiceProvider scopedServiceProvider;

        public EntityPersistenceService(ScopedServiceProvider scopedServiceProvider,
            ILogger<EntityPersistenceService> logger)
        {
            this.scopedServiceProvider = scopedServiceProvider;
            this.logger = logger;
        }

        internal async Task Add<T>(T entity) where T : class
        {
            await scopedServiceProvider.ExecuteActionInScopeAsync(
                dataContext => PersistEntity(entity, dataContext));
        }

        private async Task PersistEntity<T>(T entity, DataContext dataContext) where T : class
        {
            try
            {
                dataContext.Set<T>().Add(entity);
                await dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e, "ERROR: {ErrorMessage}", e.Message);
                throw new NightLifeException($"Something went wrong during persistence {e.StackTrace}", e);
            }
        }

        internal Task<List<T>> FindAll<T>() where T : class => scopedServiceProvider
            .ExecuteFuncInScopeAsync(dataContext => dataContext.Set<T>().ToListAsync());

        internal async Task<T> FindById<T>(int id) where T : class
        {
            T? entity = await scopedServiceProvider
                .ExecuteFuncInScopeAsync(async dataContext => await dataContext.Set<T>().FindAsync(id));
            return entity ?? throw new NightLifeException("Party Event not found!");
        }

        internal Task Update<T>(T entity) where T : class => scopedServiceProvider
            .ExecuteActionInScopeAsync(dataContext => UpdateEntity(entity, dataContext));

        private static async Task UpdateEntity<T>(T entity, DataContext dataContext) where T : class
        {
            dataContext.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
            await dataContext.SaveChangesAsync();
        }

        internal Task Delete<T>(int id) where T : class, IEntity, new() => scopedServiceProvider
            .ExecuteActionInScopeAsync(dataContext => DeleteEntity<T>(id, dataContext));

        private static async Task DeleteEntity<T>(int id, DataContext dataContext) where T : class, IEntity, new()
        {
            var entity = new T { Id = id };
            dataContext.Set<T>().Attach(entity);
            dataContext.Set<T>().Remove(entity);
            await dataContext.SaveChangesAsync();
        }

        internal Task<PartyPlace> FindByXYTime(double latitude, double longitude, DateTime dateTime)
        {
            async Task<PartyPlace> GetPartyPlaceByXYTime(DataContext dataContext)
            {
                var place = await dataContext.PartyPlaces
                    .FirstOrDefaultAsync(p => p.Latitude == latitude && p.Longitude == longitude);

                if (place != null)
                {
                    if (place.Events != null)
                    {
                        place.Events = place.Events
                        .Where(e => e.EventTime.Date == dateTime.Date)
                        .ToHashSet();
                    }
                    return place;
                }

                throw new NightLifeException("Place not found");
            }

            return scopedServiceProvider.ExecuteFuncInScopeAsync(dataContext => GetPartyPlaceByXYTime(dataContext));
        }

        internal Task<List<PartyEvent>> FindAllFilteredEvents(FilteredEventsDto filteredEvents)
        {
            return scopedServiceProvider.ExecuteFuncInScopeAsync(async dataContext =>
            {
                return await FilterEvents(filteredEvents, dataContext);
            });
        }

        private static Task<List<PartyEvent>> FilterEvents(FilteredEventsDto filteredEvents, DataContext dataContext)
        {
            if (filteredEvents.Date == null)
            {
                throw new NightLifeException("Date is missing");
            }

            Func<DataContext, Task<List<PartyEvent>>> filteredEventsFunc = FilterEventsByGenrePriceDate(filteredEvents);
            return filteredEventsFunc(dataContext);
        }

        private static Func<DataContext, Task<List<PartyEvent>>> FilterEventsByGenrePriceDate(FilteredEventsDto filteredEvents)
        {
            if (filteredEvents.Genre != null && filteredEvents.Price != null)
            {
                return async (data) =>
                {
                    return (await data.PartyEvents
                        .Where(e =>
                            e.EventTime == filteredEvents.Date &&
                            e.Genre == filteredEvents.Genre &&
                            e.Price == filteredEvents.Price)
                        .ToListAsync());
                };
            }
            else if (filteredEvents.Genre == null && filteredEvents.Price != null)
            {
                return async (data) =>
                {
                    return (await data.PartyEvents
                        .Where(e =>
                            e.EventTime == filteredEvents.Date &&
                            e.Price == filteredEvents.Price)
                        .ToListAsync());
                };
            }
            else if (filteredEvents.Genre != null)
            {
                return async (data) =>
                {
                    return (await data.PartyEvents
                        .Where(e =>
                            e.EventTime == filteredEvents.Date &&
                            e.Genre == filteredEvents.Genre)
                        .ToListAsync());
                };
            }
            else
            {
                return async (data) =>
                {
                    return (await data.PartyEvents
                        .Where(e => e.EventTime == filteredEvents.Date)
                        .ToListAsync());
                };
            }
        }

        internal Task<HashSet<AppUser>> FindGuestByPartyName(string name) =>
                scopedServiceProvider.ExecuteFuncInScopeAsync(
                    data => FindGuestsByEvent(name, data));

        private static async Task<HashSet<AppUser>> FindGuestsByEvent(string eventName, DataContext dataContext)
        {
            PartyEvent? partyEvent = await dataContext.PartyEvents
            .Include(e => e.AppUsers)
            .FirstOrDefaultAsync(e => e.Name == eventName);

            if (partyEvent?.AppUsers != null)
            {
                return partyEvent.AppUsers;
            }

            return new HashSet<AppUser>();
        }

        internal Task<List<PartyEvent>> FindAllEventsByDate(DateTime date)
        {
           return scopedServiceProvider.ExecuteFuncInScopeAsync(data => 
           data.PartyEvents.Where(e => e.EventTime.Equals(date)).ToListAsync());
        }
    }
}

