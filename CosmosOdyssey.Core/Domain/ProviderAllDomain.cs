using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosOdyssey.Core.Domain
{
    public class ProviderAllDomain
    {
        [Key]
        public string ProviderId { get; set; }
        public float Price { get; set; }
        public DateTime FlightStart { get; set; }
        public DateTime FlightEnd { get; set; }
        public string CompanyName { get; set; }
        public string CompanyId { get; set; }
        public string To { get; set; }
        public string ToId { get; set; }
        public string From { get; set; }
        public string FromId { get; set; }
        public string RouteInfoId { get; set; }
        public long Distance { get; set; }
        public string  LegId { get; set; }
        public PriceListDomain PriceListDomain { get; set; }
        [ForeignKey("PriceListDomain")]
        public string PriceListDomainId { get; set; }
    }
}
