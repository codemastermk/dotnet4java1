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
            };
        }

        internal HashSet<PartyEventDto> ConvertAllToDTO(HashSet<PartyEvent> partyEvents) => 
            partyEvents.Select(e => ConvertToDTO(e)).ToHashSet();
    }
}
