using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CosmosOdyssey.Core.Domain
{
    public class ProviderDomain
    {
        
        public string Id { get; set; }
        public CompanyDomain CompanyDomain { get; set; }
        [ForeignKey("CompanyDomain")]
        public string CompanyDomainId { get; set; }

        public float Price { get; set; }
        public DateTime FlightStart { get; set; }
        public DateTime FlightEnd { get; set; }

        public PriceListDomain PriceListDomain { get; set; }
        [ForeignKey("PriceListDomain")]
        public string PriceListDomainId { get; set; }
    }

}
