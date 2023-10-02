using night_life_sk.Dto.Event;
using night_life_sk.Models;

namespace night_life_sk.Mappers
{
    internal static class PartyEventMapper
    {
        internal static PartyEventDto ConvertToDTO(PartyEvent partyEvent) 
        {
            return new PartyEventDto(
                partyEvent.Name,
                partyEvent.Description,
                partyEvent.Genre,
                partyEvent.Price,
                partyEvent.ImageUrl,
                partyEvent.EventTime,
                partyEvent.PartyPlace?.Latitude,
                partyEvent.PartyPlace?.Longitude);
        }

        internal static HashSet<PartyEventDto> ConvertAllToDTO(Task<List<PartyEvent>> partyEvents) => 
            partyEvents.Result.Select(e => ConvertToDTO(e)).ToHashSet();
    }
}
