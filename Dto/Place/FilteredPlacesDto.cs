namespace night_life_sk.Dto.Place
{
    public record FilteredPlacesDto
    {
        public int? Price { get; set; }
        public string? Genre { get; set; }
        public DateTime? Date { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Distance { get; set; }
    }
}
