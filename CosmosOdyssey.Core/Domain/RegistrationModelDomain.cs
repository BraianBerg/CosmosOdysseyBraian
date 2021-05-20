using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosOdyssey.Core.Domain
{
    public class RegistrationModelDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public PriceListDomain PriceListDomain { get; set; }
        [ForeignKey("PriceListDomain")]
        public string PriceListDomainId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Routes { get; set; }
        public float TotalPrice { get; set; }
        public string TotalTravelTime { get; set; }
        public string Companys { get; set; }
    }
}
