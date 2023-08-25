using Microsoft.AspNetCore.Mvc;
using night_life_sk.Dto.Event;
using night_life_sk.Dto.Place;
using night_life_sk.Mappers;
using night_life_sk.Models;
using night_life_sk.Repositories.Event;
using night_life_sk.Repositories.place;

namespace night_life_sk.Services
{
    public class BaseMapService
    {
        private readonly IPartyPlaceRepository partyPlaceRepository;
        private readonly IPartyEventRepository partyEventRepository;
        private readonly PartyEventMapper partyEventMapper;
        private readonly PartyPlaceMapper partyPlaceMapper;
        
        public BaseMapService(
            PartyEventMapper eventMapper,
            PartyPlaceMapper partyPlaceMapper,
            IPartyPlaceRepository partyPlaceRepository,
            IPartyEventRepository partyEventRepository) 
        {
            this.partyEventRepository = partyEventRepository;
            this.partyEventMapper = eventMapper;
            this.partyPlaceMapper = partyPlaceMapper;
            this.partyPlaceRepository = partyPlaceRepository;
        }

        internal HashSet<PlaceCoordinates> GetAllPartyPlaces() => 
            partyPlaceMapper.ConvertAllToCoordinates(partyPlaceRepository.FindAll());

        internal HashSet<PartyEventDto> GetEventsByDate(DateTime date) =>
            partyEventMapper.ConvertAllToDTO(partyEventRepository.FindAllEventsByDate(date));

        internal HashSet<PartyEventDto> GetFilteredEvents(FilteredPlacesDto filteredPlaces) =>
            partyEventMapper.ConvertAllToDTO(partyEventRepository.FindAllFilteredEvents(filteredPlaces));

        internal PlaceAndEventDto GetPlaceAndEventOnClick(double longitude, double latitude, DateTime date) => 
            partyPlaceMapper.ConvertToOnClickClub(partyPlaceRepository.FindByXYTime(longitude, latitude, date));
    }
}
