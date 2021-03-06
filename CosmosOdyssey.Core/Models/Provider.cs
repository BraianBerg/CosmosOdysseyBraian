using System;

namespace CosmosOdyssey.Core.Models
{
    public class Provider
    {
        public string Id { get; set; }
        public Company Company { get; set; }
        public float Price { get; set; }
        public DateTime FlightStart { get; set; }
        public DateTime FlightEnd { get; set; }
    }

}
