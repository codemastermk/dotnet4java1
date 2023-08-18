﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using night_life_sk.Data;
using night_life_sk.Exceptions;
using night_life_sk.Models;
using System;

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
    }
}

