namespace night_life_sk.Dto.Event
{
    public record PartyEventDto
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? Genre { get; init; }
        public int? Price { get; init; }
        public string? ImageUrl { get; init; }
        public DateTime? EventTime { get; init; }
    }
}
