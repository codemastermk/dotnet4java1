using night_life_sk.Dto.Place;
using night_life_sk.Models;

namespace night_life_sk.Mappers
{
    public class PartyPlaceMapper
    {
        private readonly PartyEventMapper eventMapper;
        public PartyPlaceMapper(PartyEventMapper eventMapper)
        {
            this.eventMapper = eventMapper;
        }

        private static PartyPlaceDto ConvertToDTO(PartyPlace partyPlace)
        {
            return new PartyPlaceDto
            {
                Name = partyPlace.Name,
                Address = partyPlace.Address,
                Latitude = partyPlace.Latitude,
                Longitude = partyPlace.Longitude,
            };
        }

        private static PlaceCoordinates ConvertToCoordinates(PartyPlace partyPlace)
        {
            return new PlaceCoordinates
            {
                PlaceName = partyPlace.Name,
                Latitude = partyPlace.Latitude,
                Longitude = partyPlace.Longitude
            };
        }

        public HashSet<PlaceCoordinates> ConvertAllToCoordinates(HashSet<PartyPlace> partyPlaces)
        {
            return partyPlaces.Select(place => ConvertToCoordinates(place)).ToHashSet();
        }

        public HashSet<PartyPlaceDto> ConvertAllToDTO(HashSet<PartyPlace> partyPlaces)
        {
            return partyPlaces.Select(place => ConvertToDTO(place)).ToHashSet();
        }

        internal PlaceAndEventDto ConvertToOnClickClub(PartyPlace partyPlace)
        {
            PartyEvent? partyEvent = null;
            if (partyPlace.Events != null)
            {
                partyEvent = partyPlace.Events.FirstOrDefault();
            }
            if (partyEvent != null)
            {
                return new PlaceAndEventDto
                {
                    Address = partyPlace.Address,
                    Name = partyPlace.Name,
                    EventDto = eventMapper.ConvertToDTO(partyEvent),
                };
            }
            else
            {
                return new PlaceAndEventDto
                {
                    Address = partyPlace.Address,
                    Name = partyPlace.Name,
                };
            }

        }
    }
}
