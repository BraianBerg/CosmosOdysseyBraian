using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CosmosOdyssey.Core.Domain
{
    public class LegDomain
    {
        
        public string Id { get; set; }
        
        public  RouteinfoDomain RouteInfoDomain { get; set; }
        [ForeignKey("RouteInfoDomain")]
        public string RouteInfoDomainId { get; set; }
        
        public PriceListDomain PriceListDomain { get; set; }
        [ForeignKey("PriceListDomain")]
        public string PriceListDomainId { get; set; }

        public  ProviderDomain ProvidersDomain { get; set; }
        [ForeignKey("ProvidersDomain")]
        public string ProvidersDomainId { get; set; }


    }

}
