using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CosmosOdyssey.Core.Domain
{
    //[Keyless]
    public class PriceListDomain
    {
        [Key]
        public string Id { get; set; }

        public DateTime ValidUntil { get; set; }


    }
}
