using night_life_sk.Dto.Event;
using night_life_sk.Dto.Place;
using night_life_sk.Dto.User;
using night_life_sk.Mappers;
using night_life_sk.Repositories;

namespace night_life_sk.Services
{
    internal class BaseMapService
    {
        private readonly PartyPlaceRepository partyPlaceRepository;
        private readonly PartyEventRepository partyEventRepository;
        private readonly AppUserRepository appUserRepository;

        public BaseMapService(
            PartyPlaceRepository partyPlaceRepository,
            PartyEventRepository partyEventRepository,
            AppUserRepository appUserRepository) 
        {
            this.appUserRepository = appUserRepository;
            this.partyEventRepository = partyEventRepository;
            this.partyPlaceRepository = partyPlaceRepository;
        }

        internal HashSet<PlaceCoordinates> GetAllPartyPlaces() => 
            PartyPlaceMapper.ConvertAllToCoordinates(partyPlaceRepository.FindAll());

        internal HashSet<PartyEventDto> GetEventsByDate(DateTime date) =>
            PartyEventMapper.ConvertAllToDTO(partyEventRepository.FindAllEventsByDate(date));

        internal HashSet<PartyEventDto> GetFilteredEvents(FilteredEventsDto filteredEvents) =>
            PartyEventMapper.ConvertAllToDTO(partyEventRepository.FindAllFilteredEvents(filteredEvents));

        internal HashSet<AppUserDto> GetInterestedUsersForEvent(string eventName) =>
            AppUserMapper.ConvertAllToDTO(appUserRepository.FindAllByPartyName(eventName));

        internal PlaceAndEventDto GetPlaceAndEventOnClick(double longitude, double latitude, DateTime date) =>
            PartyPlaceMapper.ConvertToOnClickClub(partyPlaceRepository.FindByXYTime(longitude, latitude, date));
    }
}
