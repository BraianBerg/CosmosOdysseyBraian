using System;

namespace CosmosOdyssey.Core.Models
{
    public class Rootobject
    {
        public string Id { get; set; }
        public DateTime ValidUntil { get; set; }
        public Leg[] Legs { get; set; }
    }

}
