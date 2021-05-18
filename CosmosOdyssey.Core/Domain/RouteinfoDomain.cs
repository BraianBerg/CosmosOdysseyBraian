using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CosmosOdyssey.Core.Domain
{
    public class RouteinfoDomain
    {
        [Key]
        public string Id { get; set; }
        public FromDomain FromDomain { get; set; }
        [ForeignKey("FromDomain")]
        public string FromDomainId { get; set; }

        public ToDomain ToDomain { get; set; }
        [ForeignKey("ToDomain")]
        public string ToDomainId { get; set; }

        public long Distance { get; set; }

        public PriceListDomain PriceListDomain { get; set; }
        [ForeignKey("PriceListDomain")]
        public string PriceListDomainId { get; set; }
    }

}
