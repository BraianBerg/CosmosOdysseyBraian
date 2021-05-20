using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosOdyssey.Core.ViewModel
{
    public class RegistrationModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Routes { get; set; }
        public float TotalPrice { get; set; }
        public string TotalTravelTime { get; set; }
        public string Companys { get; set; }
        public string PriceListId { get; set; }
    }
}
