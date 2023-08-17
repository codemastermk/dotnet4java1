﻿using night_life_sk.Services.persistence;

namespace night_life_sk.Models
{
    public class PartyPlace : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set;}
        public virtual AppUser? AppUser { get; set; }
        public virtual HashSet<PartyEvent>? Events { get; set; }
    }
}
