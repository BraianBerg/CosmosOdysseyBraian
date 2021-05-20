using CosmosOdyssey.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosOdyssey.Data
{
    public class CosmosOdysseyDbContext : DbContext
    {
        public CosmosOdysseyDbContext(DbContextOptions<CosmosOdysseyDbContext> options)
            : base(options) { }
        public DbSet<IdAi> IdAi { get; set; }
        public DbSet<ProviderAllDomain> ProviderAllDomains { get; set; }
        public DbSet<RegistrationModelDomain> RegistrationModelDomain { get; set; }
        public DbSet<PriceListDomain> PriceListDomains { get; set; }
        //  public DbSet<ToDomain> ToDomains { get; set; }
        //   public DbSet<CompanyDomain> CompanyDomains { get; set; }
        //    public DbSet<FromDomain> FromDomains { get; set; }
        //   public DbSet<LegDomain> LegDomains { get; set; }
        //  public DbSet<ProviderDomain> ProviderDomains { get; set; }
        //   public DbSet<RouteinfoDomain> RouteinfoDomains { get; set; }



    }
}
