namespace CosmosOdyssey.Core.Models
{
    public class Leg
    {
        public string Id { get; set; }
        public Routeinfo RouteInfo { get; set; }
        public Provider[] Providers { get; set; }
    }

}
