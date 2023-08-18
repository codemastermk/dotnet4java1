﻿using night_life_sk.Services.persistence;

namespace night_life_sk.Models
{
    public class PartyEvent : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Genre { get; set; }
        public int? Price { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? EventTime { get; set; }
        public virtual PartyPlace? PartyPlace { get; set; }
    }
}
