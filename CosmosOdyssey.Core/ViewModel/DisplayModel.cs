using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosOdyssey.Core.ViewModel
{
    public class DisplayModel
    {
        public string PriceListId { get; set; }
        public string Form { get; set; }
        public string To { get; set; }
        public DateTime FlightStart { get; set; }
        public DateTime FlightEnd { get; set; }
        public string CompanyName { get; set; }
        public TimeSpan TravelTime { get; set; }

    }
}
