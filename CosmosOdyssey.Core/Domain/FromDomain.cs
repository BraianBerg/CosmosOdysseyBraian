using System.ComponentModel.DataAnnotations.Schema;

namespace CosmosOdyssey.Core.Domain
{
    public class FromDomain
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public PriceListDomain PriceListDomain { get; set; }
        [ForeignKey("PriceListDomain")]
        public string PriceListDomainId { get; set; }
    }

}
