using CosmosOdyssey.Core.Domain;
using CosmosOdyssey.Core.Models;
using CosmosOdyssey.Data;
using CosmosOdyssey.Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;


namespace CosmosOdyssey.ApplicationServices
{


    public class PriceListServices : IPriceListServices
    {

        private readonly CosmosOdysseyDbContext _context;
        private readonly IHttpClientFactory _clientFactory;

        public PriceListServices(IHttpClientFactory clientFactory, CosmosOdysseyDbContext context)
        {
            _clientFactory = clientFactory;
            _context = context;
        }
        //Task<ActionResult<PriceListDomain>>

        public async void PostRootData()
        {



            // var client = _clientFactory.CreateClient("meta");
            Rootobject rootobject = new Rootobject();
            //  rootobject = await client.GetFromJsonAsync<Rootobject>("v1.0/TravelPrices");
            rootobject = await GetDataFromJson();

            // outgoing data to db
            PriceListDomain priceList = new PriceListDomain();
        //    LegDomain leg = new LegDomain();
        //    ProviderDomain provider = new ProviderDomain();
        //    RouteinfoDomain routeinfo = new RouteinfoDomain();
         //   ToDomain to = new ToDomain();
          //  FromDomain from = new FromDomain();
         //   CompanyDomain company = new CompanyDomain();
            IdAi aiid = new IdAi();
            

            //rootobj
            priceList.Id = rootobject.Id;
            priceList.ValidUntil = rootobject.ValidUntil;



            // takes compnayid from db
        /*    var CompanyChek = _context.CompanyDomains;
            List<string> cchek = new List<string>();
            foreach (var comp in CompanyChek)
            {
                cchek.Add(comp.Id);
            }
        */
            //cheks if pricelist is dupicate or not
            var PriceIdChek = _context.PriceListDomains;
            List<string> PriceId = new List<string>();
            foreach (var key in PriceIdChek)
            {
                PriceId.Add(key.Id);
            }
            if (!PriceId.Contains(rootobject.Id))
            {
                _context.PriceListDomains.Add(priceList);
                //   priceList = new PriceListDomain();
                await _context.SaveChangesAsync();

                aiid.PriceListDomainId = priceList.Id;
                _context.IdAi.Add(aiid);
                await _context.SaveChangesAsync();

                ProviderAllDomain providerAll = new ProviderAllDomain();
                for (int t = 0; t < rootobject.Legs.Length; t++)
                {

                    for (int i = 0; i < rootobject.Legs[t].Providers.Length; i++)
                    {

                        providerAll.Distance = rootobject.Legs[t].RouteInfo.Distance;
                        providerAll.PriceListDomainId = priceList.Id;
                        providerAll.LegId = rootobject.Legs[t].Id;
                        providerAll.RouteInfoId = rootobject.Legs[t].RouteInfo.Id;
                        providerAll.From = rootobject.Legs[t].RouteInfo.From.Name;
                        providerAll.FromId = rootobject.Legs[t].RouteInfo.From.Id;
                        providerAll.To = rootobject.Legs[t].RouteInfo.To.Name;
                        providerAll.ToId = rootobject.Legs[t].RouteInfo.To.Id;

                        providerAll.ProviderId = rootobject.Legs[t].Providers[i].Id;
                        providerAll.Price = rootobject.Legs[t].Providers[i].Price;
                        providerAll.CompanyId = rootobject.Legs[t].Providers[i].Company.Id;
                        providerAll.CompanyName = rootobject.Legs[t].Providers[i].Company.Name;
                        providerAll.FlightStart = rootobject.Legs[t].Providers[i].FlightStart;
                        providerAll.FlightEnd = rootobject.Legs[t].Providers[i].FlightEnd;

                       _context.ProviderAllDomains.Add(providerAll);
                        await _context.SaveChangesAsync();
                    }
                }

               
                //  _context.PriceListDomains.OrderByDescending(o => o.Key).FirstOrDefault();
                //   var test = _context.PriceListDomains.FirstOrDefault(); 
                //legs array loop first dimension
                /*
                for (int t = 0; t < rootobject.Legs.Length; t++)
                {


                    //leg class 
                    leg.PriceListDomainId = priceList.Id;
                    leg.Id = rootobject.Legs[t].Id;
                    leg.RouteInfoDomainId = rootobject.Legs[t].RouteInfo.Id;



                    //routeinfo //Leg[0]
                    routeinfo.Id = rootobject.Legs[t].RouteInfo.Id;
                    routeinfo.FromDomainId = rootobject.Legs[t].RouteInfo.From.Id;
                    routeinfo.ToDomainId = rootobject.Legs[t].RouteInfo.To.Id;
                    routeinfo.Distance = rootobject.Legs[t].RouteInfo.Distance;
                    routeinfo.PriceListDomainId = priceList.Id;
                    //to // routeinfo // leg[0]
                    to.Id = rootobject.Legs[t].RouteInfo.To.Id;
                    to.Name = rootobject.Legs[t].RouteInfo.To.Name;
                    to.PriceListDomainId = priceList.Id;
                    // from // routeinfo // leg[0]
                    from.Id = rootobject.Legs[t].RouteInfo.From.Id;
                    from.Name = rootobject.Legs[t].RouteInfo.From.Name;
                    from.PriceListDomainId = priceList.Id;
                    // providerarray loop secon dimension 
                    for (int i = 0; i < rootobject.Legs[t].Providers.Length; i++)
                    {
                        // provider[0] // leg[0]
                        provider.Id = rootobject.Legs[t].Providers[i].Id;
                        provider.CompanyDomainId = rootobject.Legs[t].Providers[i].Company.Id;
                        provider.Price = rootobject.Legs[t].Providers[i].Price;
                        provider.FlightStart = rootobject.Legs[t].Providers[i].FlightStart;
                        provider.FlightEnd = rootobject.Legs[t].Providers[i].FlightEnd;
                        provider.PriceListDomainId = priceList.Id;


                        //company  // provider[0] // leg[0]
                        company.Id = rootobject.Legs[t].Providers[i].Company.Id;
                        company.Name = rootobject.Legs[t].Providers[i].Company.Name;
                        company.PriceListDomainId = priceList.Id;
                        if (cchek.Contains(rootobject.Legs[t].Providers[i].Company.Id))
                        {

                        }
                        else
                        {
                            cchek.Add(rootobject.Legs[t].Providers[i].Company.Id);
                            _context.CompanyDomains.Add(company);
                        }
                        _context.ProviderDomains.Add(provider);
                        await _context.SaveChangesAsync();
                        company = new CompanyDomain();
                        provider = new ProviderDomain();

                    }

                    _context.FromDomains.Add(from);
                    from = new FromDomain();
                    _context.ToDomains.Add(to);
                    to = new ToDomain();
                    _context.RouteinfoDomains.Add(routeinfo);
                    routeinfo = new RouteinfoDomain();
                    _context.LegDomains.Add(leg);
                    leg = new LegDomain();
                    await _context.SaveChangesAsync();
                }
                //  await _context.SaveChangesAsync();
                //  await _context.SaveChangesAsync();

                await _context.SaveChangesAsync();
                */
            }
            else
            {
                Console.WriteLine("this pricelist is allready in database");
            }

        }
        // will get data from api and pass it in to models
        public async Task<Rootobject> GetDataFromJson()
        {
            var client = _clientFactory.CreateClient("meta");
            Rootobject rootobject = new Rootobject();
            rootobject = await client.GetFromJsonAsync<Rootobject>("v1.0/TravelPrices");

            return rootobject;
        }

        
        public async Task<DisplayModel[]> GetSpaceTravelDataDomain()
        {
            DisplayModel[] displayModel = new DisplayModel[300];
            for (int i = 0; i < displayModel.Length; i++)
            {
                displayModel[i] = new DisplayModel();
            }
           

            var stuffid = await _context.IdAi.OrderByDescending(e => e.Id).FirstOrDefaultAsync();
            var shit = _context.ProviderAllDomains.Where(e => e.PriceListDomainId.Contains(stuffid.PriceListDomainId));
            // IQueryable<LegDomain> query = _context.LegDomains;
            //  query.OrderByDescending(LegDomain.);
            if (stuffid.PriceListDomainId != null)
            {
                int i = 0;
                foreach (var item in shit)
                {

                    displayModel[i].Form = item.From;
                    displayModel[i].To = item.To;
                    displayModel[i].FlightStart = item.FlightStart;
                    displayModel[i].FlightEnd = item.FlightEnd;
                    displayModel[i].PriceListId = item.PriceListDomainId;
                    displayModel[i].CompanyName = item.CompanyName;
                    displayModel[i].TravelTime = displayModel[i].FlightEnd.Subtract(displayModel[i].FlightStart);
                    i++;
                }
              //  displayModel[0] = (DisplayModel)_context.ProviderAllDomains.Where(e => e.PriceListDomainId.Contains(stuffid.PriceListDomainId));

           




                // displayModel.CompanyName = c 


                //query = query.Where(e => e.Id.Contains(stuffid.PriceListDomainId));
                //     .Include(e => e.ProvidersDomain)
                //   .ThenInclude(e => e.CompanyDomain);

            }
            // List<LegDomain> leg = query.ToList();
            return displayModel;
            //await query.ToListAsync();
        }
        
        /*
        ///delete shit based on id 
        public async void DeleteOldPriceList(string id)
        {
            var priceList = await _context.PriceListDomains.FindAsync(id);
            if (priceList == null)
            {

                Console.WriteLine("sellist asja ple siin");
            }

            _context.PriceListDomains.Remove(priceList);
            await _context.SaveChangesAsync();
        }
        */
































        ///test to get data from database
        /* public async Task<PriceListDomain> GetCurrentData()
         {
             Rootobject rootobject = new Rootobject();
             rootobject = await GetDataFromJson();
             var proov = _context.PriceListDomains;


             return await _context.LegDomains
                 .FirstOrDefault( e => e.PriceListDomainId ==  )

         }*/

















        /*
        Rootobject IPriceListServices.GetPriceList()
        {
            var client = new RestClient($"https://cosmos-odyssey.azurewebsites.net/api/v1.0/TravelPrices");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                // Deserialize the string content into JToken object
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                // Deserialize the JToken object into our WeatherResponse Class
                return content.ToObject<Rootobject>();
            }
            return null;
        }
        */



    }
}
