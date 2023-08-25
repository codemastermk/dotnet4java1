using night_life_sk.Dto.Event;
using night_life_sk.Models;

namespace night_life_sk.Mappers
{
    public class PartyEventMapper
    {
        public PartyEventDto ConvertToDTO(PartyEvent partyEvent) 
        {
            return new PartyEventDto
            {
                Description = partyEvent.Description,
                EventTime = partyEvent.EventTime,
                Genre = partyEvent.Genre,
                ImageUrl = partyEvent.ImageUrl,
                Name = partyEvent.Name,
                Price = partyEvent.Price,
                Latitude = partyEvent.PartyPlace != null ? partyEvent.PartyPlace.Latitude : null,
                Longitude = partyEvent.PartyPlace != null ? partyEvent.PartyPlace.Longitude : null
            };
        }

        internal HashSet<PartyEventDto> ConvertAllToDTO(HashSet<PartyEvent> partyEvents) => 
            partyEvents.Select(e => ConvertToDTO(e)).ToHashSet();
    }
}
