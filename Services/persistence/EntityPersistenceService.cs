using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using night_life_sk.Data;
using night_life_sk.Dto.Place;
using night_life_sk.Exceptions;
using night_life_sk.Models;
using System;
using Dapper;


namespace night_life_sk.Services.persistence
{
    public interface IEntity
    {
        int Id { get; set; }
    }

    public class EntityPersistenceService
    {
        private readonly ScopedServiceProvider scopedServiceProvider;

        public EntityPersistenceService(ScopedServiceProvider scopedServiceProvider)
        {
            this.scopedServiceProvider = scopedServiceProvider;
        }

        public void Add<T>(T entity) where T : class => scopedServiceProvider.
            ExecuteActionInScope(dataContext => PersistEntity(entity, dataContext));

        private static void PersistEntity<T>(T entity, DataContext dataContext) where T : class
        {
            dataContext.Set<T>().Add(entity);
            dataContext.SaveChanges();
        }

        public HashSet<T> FindAll<T>() where T : class => scopedServiceProvider
            .ExecuteFuncInScope(dataContext => dataContext.Set<T>().ToHashSet());

        public T FindById<T>(int id) where T : class
        {
            T? entity = scopedServiceProvider
                .ExecuteFuncInScope(dataContext => dataContext.Set<T>().Find(id));
            return entity ?? throw new NightLifeException("Party Event not found!");
        }

        public void Update<T>(T entity) where T : class => scopedServiceProvider
            .ExecuteActionInScope(dataContext => UpdateEntity(entity, dataContext));

        private static void UpdateEntity<T>(T entity, DataContext dataContext) where T : class
        {
            dataContext.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
            dataContext.SaveChanges();
        }

        public void Delete<T>(int id) where T : class, IEntity, new() => scopedServiceProvider
            .ExecuteActionInScope(dataContext => DeleteEntity<T>(id, dataContext));

        private static void DeleteEntity<T>(int id, DataContext dataContext) where T : class, IEntity, new()
        {
            var entity = new T { Id = id };
            dataContext.Set<T>().Attach(entity);
            dataContext.Set<T>().Remove(entity);
            dataContext.SaveChanges();
        }

        public PartyPlace FindByXYTime(double latitude, double longitude, DateTime dateTime)
        {
            PartyPlace GetPartyPlaceByXYTime(DataContext dataContext)
            {
                var place = dataContext.PartyPlaces
                    .FirstOrDefault(p => p.Latitude == latitude && p.Longitude == longitude);

                if (place != null)
                {
                    if (place.Events != null)
                    {
                        place.Events = place.Events
                        .Where(e => e.EventTime.HasValue && e.EventTime.Value.Date == dateTime.Date)
                        .ToHashSet();
                    }
                    return place;
                }

                throw new NightLifeException("Place not found");
            }

            return scopedServiceProvider.ExecuteFuncInScope(dataContext => GetPartyPlaceByXYTime(dataContext));
        }

        internal HashSet<PartyEvent> FindAllEventsByDate(DateTime date) =>
            scopedServiceProvider.ExecuteFuncInScope(
                dataContext => dataContext.PartyEvents
                    .Where(e => e.EventTime.HasValue && e.EventTime.Value.Date == date)
                    .ToHashSet());

        internal HashSet<PartyEvent> FindAllFilteredEvents(FilteredPlacesDto filteredPlaces) =>
            scopedServiceProvider.ExecuteFuncInScope(dataContext => FilterEvents(filteredPlaces, dataContext));

        private static HashSet<PartyEvent> FilterEvents(FilteredPlacesDto filteredPlaces, DataContext dataContext)
        {
            if (filteredPlaces.Date == null)
            {
                throw new NightLifeException("Date is missing");
            }

            var places = new HashSet<PartyPlace>();

            if (
                filteredPlaces.Distance != null &&
                filteredPlaces.Latitude != null &&
                filteredPlaces.Longitude != null)
            {
                FilterNearbyPlaces(
                    filteredPlaces.Latitude,
                    filteredPlaces.Longitude,
                    filteredPlaces.Distance,
                    ref places);
            }

            if (places.Count > 0)
            {
                Func<DataContext, PartyPlace, HashSet<PartyEvent>> filteredEvents = FilterEventsByGenrePriceDate(filteredPlaces);
                HashSet<PartyEvent> events = new();
                foreach (var place in places)
                {
                    events.UnionWith(filteredEvents(dataContext, place));
                }
            }

            return new HashSet<PartyEvent>();
        }

        private static Func<DataContext, PartyPlace, HashSet<PartyEvent>> FilterEventsByGenrePriceDate(FilteredPlacesDto filteredPlaces)
        {

            if (filteredPlaces.Genre != null && filteredPlaces.Price != null)
            {
                return (data, partyPlace) => data.PartyEvents
                .Where(
                    e => e.PartyPlace != null &&
                    e.EventTime == filteredPlaces.Date &&
                    e.PartyPlace.Equals(partyPlace) &&
                    e.Genre == filteredPlaces.Genre &&
                    e.Price == filteredPlaces.Price)
                .ToHashSet();
            }
            else if (filteredPlaces.Genre == null && filteredPlaces.Price != null)
            {
                return (data, partyPlace) => data.PartyEvents
                .Where(
                    e => e.PartyPlace != null &&
                    e.EventTime == filteredPlaces.Date &&
                    e.PartyPlace.Equals(partyPlace) &&
                    e.Price == filteredPlaces.Price)
                .ToHashSet();
            }
            else if (filteredPlaces.Genre != null)
            {
                return (data, partyPlace) => data.PartyEvents
                .Where(
                    e => e.PartyPlace != null &&
                    e.EventTime == filteredPlaces.Date &&
                    e.PartyPlace.Equals(partyPlace) &&
                    e.Genre == filteredPlaces.Genre)
                .ToHashSet();
            } 
            else
            {
                return (data, partyPlace) => data.PartyEvents
                .Where(
                    e => e.PartyPlace != null &&
                    e.EventTime == filteredPlaces.Date)
                .ToHashSet();
            }
        }

        private static void FilterNearbyPlaces(
            double? latitude,
            double? longitude,
            double? distance,
            ref HashSet<PartyPlace> places)
        {
            // Use Haversine formula in SQL query
            string query = @"
            SELECT PlaceID, Name, Latitude, Longitude 
            FROM Places 
            WHERE (6371 * acos(
            cos(radians(@lat)) 
            * cos(radians(Latitude)) 
            * cos(radians(Longitude) - radians(@lng)) 
            + sin(radians(@lat)) 
            * sin(radians(Latitude))
        )) <= @distance";

            using SqlConnection connection = new(
                "Data Source = pc676; Initial Catalog = night_life; Integrated Security = True;" +
                " Connect Timeout = 30; Encrypt = False; " +
                "Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False");
                places = new HashSet<PartyPlace>(
                    connection.Query<PartyPlace>(
                        query, new { lat = latitude, lng = longitude, distance }));    
        }
    }
}

