using night_life_sk.Dto.Event;

namespace night_life_sk.Dto.Place
{
    public record PlaceAndEventDto
    {
        internal string? Name { get; init; }
        internal string? Address { get; init; }
        internal PartyEventDto? EventDto { get; init; }
    }
}
