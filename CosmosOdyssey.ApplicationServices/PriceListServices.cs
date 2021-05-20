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
using System.Data;
using System.Configuration;
using System.Threading;

namespace CosmosOdyssey.ApplicationServices
{


    public class PriceListServices : IPriceListServices
    {

        private readonly CosmosOdysseyDbContext _context;
        private readonly CosmosOdysseyDbContext Context;
        private readonly IHttpClientFactory _clientFactory;

        public PriceListServices(IHttpClientFactory clientFactory, CosmosOdysseyDbContext context, CosmosOdysseyDbContext Context)
        {
            _clientFactory = clientFactory;
            _context = context;
            this.Context = Context;
        }
        //Task<ActionResult<PriceListDomain>>

        public async void PostRootData()
        {
            //CancellationToken cancelationToken
            //!cancelationToken.IsCancellationRequested
            if (true)
            {

                int chek = _context.IdAi.Count();
                // should be locked to 15
                if (chek > 15)
                {
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!");
                    var row = await _context.IdAi.OrderBy(e => e.Id).FirstOrDefaultAsync();
                    var priceListId = await _context.IdAi.FindAsync(row.Id);


                    IdAi test3 = await _context.IdAi.FirstOrDefaultAsync(x => x.PriceListDomainId == priceListId.PriceListDomainId);

                    PriceListDomain test4 = await _context.PriceListDomains.FirstOrDefaultAsync(x => x.Id == priceListId.PriceListDomainId);
                    if (test3 != null)
                    {
                        _context.IdAi.Remove(test3);
                        await _context.SaveChangesAsync();
                    }

                    RegistrationModelDomain test2 = new RegistrationModelDomain();
                    while (test2 != null)
                    {
                        test2 = await _context.RegistrationModelDomain.FirstOrDefaultAsync(x => x.PriceListDomainId == priceListId.PriceListDomainId);
                        if (test2 != null)
                        {
                            _context.RegistrationModelDomain.Remove(test2);
                            await _context.SaveChangesAsync();
                        }


                    }

                    ProviderAllDomain? test1 = new ProviderAllDomain();
                    while (test1 != null)
                    {
                        test1 = await _context.ProviderAllDomains.FirstOrDefaultAsync(x => x.PriceListDomainId == priceListId.PriceListDomainId);
                        if (test1 != null)
                        {
                            _context.ProviderAllDomains.Remove(test1);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            break;
                        }

                    }

                    if (test4 != null)
                    {
                        _context.PriceListDomains.Remove(test4);
                        await _context.SaveChangesAsync();

                    }
                    Console.WriteLine("some stuff was deleted");













                    /*

                    int Key = row.Id;

                    if (priceListId == null)
                    {

                        Console.WriteLine("sellist asja ple siin");
                    }
                    var priceList = await _context.PriceListDomains.FindAsync(priceListId.PriceListDomainId);
                    if (priceList == null)
                    {
                        Console.WriteLine("kuidas see sai juhtuda");
                    }
                    else
                    {
                        _context.IdAi.Attach(row);
                        _context.IdAi.Remove(row);
                        await _context.SaveChangesAsync();
                        _context.PriceListDomains.Attach(priceList);
                        _context.PriceListDomains.Remove(priceList);

                        */

                    // _context.PriceListDomains.Remove(await _context.ProviderAllDomains.FindAsync(e=>e.PriceListDomainId == priceList.Id));


                    //  await _context.SaveChangesAsync();

                    // var providerpricelist = _context.ProviderAllDomains.Where(e => e.PriceListDomainId.Contains(priceListId.PriceListDomainId));
                    /*   var providerpricelistmapped = new ProviderAllDomain();
                       foreach (var item in providerpricelist)
                       {
                           providerpricelistmapped.ProviderId = item.ProviderId;
                           providerpricelistmapped.Price = item.Price;
                           providerpricelistmapped.FlightStart = item.FlightStart;
                           providerpricelistmapped.FlightEnd = item.FlightEnd;
                           providerpricelistmapped.CompanyName = item.CompanyName;
                           providerpricelistmapped.CompanyId = item.CompanyId;
                           providerpricelistmapped.To = item.To;
                           providerpricelistmapped.ToId = item.ToId;
                           providerpricelistmapped.From = item.From;
                           providerpricelistmapped.FromId = item.FromId;
                           providerpricelistmapped.RouteInfoId = item.RouteInfoId;
                           providerpricelistmapped.Distance = item.Distance;
                           providerpricelistmapped.LegId = item.LegId;
                           providerpricelistmapped.PriceListDomainId = item.PriceListDomainId;

                       }
                       //  _context.ProviderAllDomains.Attach(providerpricelistmapped);
                       _context.ProviderAllDomains.Remove(providerpricelistmapped);
                       await _context.SaveChangesAsync();

                       */










                    //}
                }
                else
                {
                    Console.WriteLine("No need to delete stuff");
                }



















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
                    //////
                  
                   

                    /////
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
  

        }
        // will get data from api and pass it in to models
        public async Task<Rootobject> GetDataFromJson()
        {
            var client = _clientFactory.CreateClient("meta");
            Rootobject rootobject = new Rootobject();
            rootobject = await client.GetFromJsonAsync<Rootobject>("v1.0/TravelPrices");

            return rootobject;
        }

        public async Task<DateTime> GetValidUntil()
        {
            var row = await _context.IdAi.OrderByDescending(e => e.Id).FirstOrDefaultAsync();
            var row2 = _context.PriceListDomains.Where(e => e.Id.Contains(row.PriceListDomainId));
            DateTime validUntil = new DateTime();
            foreach (var item in row2)
            {
                validUntil = item.ValidUntil;
            }

            return validUntil;
        }

        public async Task<List<RegistrationModel>> GetRegisteredPeople()
        {
            List<RegistrationModel> registeredpeople = new List<RegistrationModel>();
            var RegPeople = await _context.RegistrationModelDomain.ToListAsync();
            foreach (var item in RegPeople)
            {
                registeredpeople.Add(new RegistrationModel
                {
                    Routes = item.Routes,
                    Companys = item.Companys,
                    TotalPrice = item.TotalPrice,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    TotalTravelTime= item.TotalTravelTime

                });
            }


            return registeredpeople;
        }

        ///deletes last record based on AIID id 
        public async void ChekAndDeleteOldPriceList()
        {
            int chek = _context.IdAi.Count();
            // should be locked to 15
            if (chek > 0)
            {
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!");
                var row = await _context.IdAi.OrderBy(e => e.Id).FirstOrDefaultAsync();
                var priceListId = await _context.IdAi.FindAsync(row.Id);

               
                IdAi test3 = await _context.IdAi.FirstOrDefaultAsync(x => x.PriceListDomainId == priceListId.PriceListDomainId);
               
                PriceListDomain  test4 = await _context.PriceListDomains.FirstOrDefaultAsync(x => x.Id == priceListId.PriceListDomainId);
                if (test3 != null)
                {
                    _context.IdAi.Remove(test3);
                    await _context.SaveChangesAsync();
                }

                RegistrationModelDomain test2 = new RegistrationModelDomain();
                while (test2 != null)
                {
                    test2 = await _context.RegistrationModelDomain.FirstOrDefaultAsync(x => x.PriceListDomainId == priceListId.PriceListDomainId);
                    if (test2 != null)
                    {
                    _context.RegistrationModelDomain.Remove(test2);
                    await _context.SaveChangesAsync();
                    }

                       
                }

                 ProviderAllDomain ? test1 = new ProviderAllDomain();
                while (test1 != null)
                {
                     test1  = await _context.ProviderAllDomains.FirstOrDefaultAsync(x => x.PriceListDomainId == priceListId.PriceListDomainId);
                    if (test1 != null)
                    {
                        _context.ProviderAllDomains.Remove(test1);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        break;
                    }
                 
                }

                if (test4 != null)
                {
                    _context.PriceListDomains.Remove(test4);
                    await _context.SaveChangesAsync();

                }
                Console.WriteLine("some stuff was deleted");













                /*

                int Key = row.Id;
              
                if (priceListId == null)
                {

                    Console.WriteLine("sellist asja ple siin");
                }
                var priceList = await _context.PriceListDomains.FindAsync(priceListId.PriceListDomainId);
                if (priceList == null)
                {
                    Console.WriteLine("kuidas see sai juhtuda");
                }
                else
                {
                    _context.IdAi.Attach(row);
                    _context.IdAi.Remove(row);
                    await _context.SaveChangesAsync();
                    _context.PriceListDomains.Attach(priceList);
                    _context.PriceListDomains.Remove(priceList);

                    */

                    // _context.PriceListDomains.Remove(await _context.ProviderAllDomains.FindAsync(e=>e.PriceListDomainId == priceList.Id));


                    //  await _context.SaveChangesAsync();

                    // var providerpricelist = _context.ProviderAllDomains.Where(e => e.PriceListDomainId.Contains(priceListId.PriceListDomainId));
                 /*   var providerpricelistmapped = new ProviderAllDomain();
                    foreach (var item in providerpricelist)
                    {
                        providerpricelistmapped.ProviderId = item.ProviderId;
                        providerpricelistmapped.Price = item.Price;
                        providerpricelistmapped.FlightStart = item.FlightStart;
                        providerpricelistmapped.FlightEnd = item.FlightEnd;
                        providerpricelistmapped.CompanyName = item.CompanyName;
                        providerpricelistmapped.CompanyId = item.CompanyId;
                        providerpricelistmapped.To = item.To;
                        providerpricelistmapped.ToId = item.ToId;
                        providerpricelistmapped.From = item.From;
                        providerpricelistmapped.FromId = item.FromId;
                        providerpricelistmapped.RouteInfoId = item.RouteInfoId;
                        providerpricelistmapped.Distance = item.Distance;
                        providerpricelistmapped.LegId = item.LegId;
                        providerpricelistmapped.PriceListDomainId = item.PriceListDomainId;

                    }
                    //  _context.ProviderAllDomains.Attach(providerpricelistmapped);
                    _context.ProviderAllDomains.Remove(providerpricelistmapped);
                    await _context.SaveChangesAsync();

                    */










               //}
                }
            else
            {
                Console.WriteLine("No need to delete stuff");
            }

        }

        public async Task<List<DisplayModel>> SearchForFlight(string FromSearchStirng, string ToSearchString, string? Company)
       {


            var PriceListId = await _context.IdAi.OrderByDescending(e => e.Id).FirstOrDefaultAsync();
            var midagi  = _context.ProviderAllDomains.Where(e => e.From.Contains(FromSearchStirng) && e.To.Contains(ToSearchString) && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));
            int NumberOfMachingData = _context.ProviderAllDomains.Where(e => e.From.Contains(FromSearchStirng) && e.To.Contains(ToSearchString) && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId)).Count();
            List<DisplayModel> displayModel = new List<DisplayModel>();
           
            if (PriceListId.PriceListDomainId != null)
            {
                if (NumberOfMachingData != 0)
                {
                    if (Company == null)
                    {
                        foreach (var item in midagi)
                        {
                            displayModel.Add(new DisplayModel
                            {
                                ProviderId = item.ProviderId,
                                Form = item.From,
                                To = item.To,
                                FlightStart = item.FlightStart,
                                FlightEnd = item.FlightEnd,
                                PriceListId = item.PriceListDomainId,
                                CompanyName = item.CompanyName,
                                Price = item.Price,
                                TravelTime = item.FlightEnd.Subtract(item.FlightStart)
                            });
                        }
                    }
                    else
                    {
                        var midagi2 = _context.ProviderAllDomains.Where(e => e.From.Contains(FromSearchStirng) && e.To.Contains(ToSearchString) && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId) && e.CompanyName.Contains(Company));
                        foreach (var item in midagi2)
                        {
                            displayModel.Add(new DisplayModel
                            {
                                ProviderId = item.ProviderId,
                                Form = item.From,
                                To = item.To,
                                FlightStart = item.FlightStart,
                                FlightEnd = item.FlightEnd,
                                PriceListId = item.PriceListDomainId,
                                CompanyName = item.CompanyName,
                                Price = item.Price,
                                TravelTime = item.FlightEnd.Subtract(item.FlightStart)
                            });
                        }
                    }
                }
                   
                else 
                {
                    var midagi1 = _context.ProviderAllDomains.Where(e => e.From.Contains(FromSearchStirng) && e.To.Contains(ToSearchString) && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));
                  
                }
               

                
            }

            return displayModel;



















            /*
             var Leg0 = _context.ProviderAllDomains.Where(e => e.From.Contains("Earth") && e.To.Contains("Jupiter") && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));
             var Leg1 = _context.ProviderAllDomains.Where(e => e.From.Contains("Earth") && e.To.Contains("Uranus") && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));

             var Leg2 = _context.ProviderAllDomains.Where(e => e.From.Contains("Mercury") && e.To.Contains("Venus") && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));

             var Leg3 = _context.ProviderAllDomains.Where(e => e.From.Contains("Venus") && e.To.Contains("Mercury") && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));         
             var Leg4 = _context.ProviderAllDomains.Where(e => e.From.Contains("Venus") && e.To.Contains("Earth") && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));

             var Leg5 = _context.ProviderAllDomains.Where(e => e.From.Contains("Mars") && e.To.Contains("Venus") && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));

             var Leg6 = _context.ProviderAllDomains.Where(e => e.From.Contains("Jupiter") && e.To.Contains("Mars") && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));
             var Leg7 = _context.ProviderAllDomains.Where(e => e.From.Contains("Jupiter") && e.To.Contains("Venus") && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));

             var Leg8 = _context.ProviderAllDomains.Where(e => e.From.Contains("Saturn") && e.To.Contains("Earth") && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));
             var Leg9 = _context.ProviderAllDomains.Where(e => e.From.Contains("Saturn") && e.To.Contains("Neptune") && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));

             var Leg10 = _context.ProviderAllDomains.Where(e => e.From.Contains("Uranus") && e.To.Contains("Saturn") && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));
             var Leg11 = _context.ProviderAllDomains.Where(e => e.From.Contains("Uranus") && e.To.Contains("Neptune") && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));

             var Leg12 = _context.ProviderAllDomains.Where(e => e.From.Contains("Neptune") && e.To.Contains("Uranus") && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));
             var Leg13 = _context.ProviderAllDomains.Where(e => e.From.Contains("Neptune") && e.To.Contains("Mercury") && e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));


             switch (FromSearchStirng)
             {
                     case "Neptune":
                 {
                     switch (ToSearchString)
                     {
                         case "Venus":
                         {


                                 int R = 0;                                    
                             foreach (var item2 in Leg13)
                             {
                                 displayModel[R].Form = item2.From;
                                 displayModel[R].To = item2.To;
                                 displayModel[R].FlightStart = item2.FlightStart;
                                 displayModel[R].FlightEnd = item2.FlightEnd;
                                 displayModel[R].PriceListId = item2.PriceListDomainId;
                                 displayModel[R].CompanyName = item2.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;
                             }
                             foreach (var item in Leg2)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             break;
                         }
                         case "Earth":
                         {
                             int R = 0;
                             foreach (var item2 in Leg13)
                             {
                                 displayModel[R].Form = item2.From;
                                 displayModel[R].To = item2.To;
                                 displayModel[R].FlightStart = item2.FlightStart;
                                 displayModel[R].FlightEnd = item2.FlightEnd;
                                 displayModel[R].PriceListId = item2.PriceListDomainId;
                                 displayModel[R].CompanyName = item2.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;
                             }
                             foreach (var item in Leg2)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg4)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }


                                 break;
                         }
                         case "Mars":
                         {
                             int R = 0;
                             foreach (var item2 in Leg13)
                             {
                                 displayModel[R].Form = item2.From;
                                 displayModel[R].To = item2.To;
                                 displayModel[R].FlightStart = item2.FlightStart;
                                 displayModel[R].FlightEnd = item2.FlightEnd;
                                 displayModel[R].PriceListId = item2.PriceListDomainId;
                                 displayModel[R].CompanyName = item2.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;
                             }
                             foreach (var item in Leg2)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg4)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg0)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg6)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }

                             break;
                         }
                         case "Jupiter":
                         {
                             int R = 0;
                             foreach (var item2 in Leg13)
                             {
                                 displayModel[R].Form = item2.From;
                                 displayModel[R].To = item2.To;
                                 displayModel[R].FlightStart = item2.FlightStart;
                                 displayModel[R].FlightEnd = item2.FlightEnd;
                                 displayModel[R].PriceListId = item2.PriceListDomainId;
                                 displayModel[R].CompanyName = item2.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;
                             }
                             foreach (var item in Leg2)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg4)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg0)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             break;
                         }
                         case "Uranus":
                         {
                             int R = 0;
                             foreach (var item2 in Leg13)
                             {
                                 displayModel[R].Form = item2.From;
                                 displayModel[R].To = item2.To;
                                 displayModel[R].FlightStart = item2.FlightStart;
                                 displayModel[R].FlightEnd = item2.FlightEnd;
                                 displayModel[R].PriceListId = item2.PriceListDomainId;
                                 displayModel[R].CompanyName = item2.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;
                             }
                             foreach (var item in Leg2)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg4)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg1)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             break;
                         }
                         case "Saturn":
                         {
                             int R = 0;
                             foreach (var item2 in Leg13)
                             {
                                 displayModel[R].Form = item2.From;
                                 displayModel[R].To = item2.To;
                                 displayModel[R].FlightStart = item2.FlightStart;
                                 displayModel[R].FlightEnd = item2.FlightEnd;
                                 displayModel[R].PriceListId = item2.PriceListDomainId;
                                 displayModel[R].CompanyName = item2.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;
                             }
                             foreach (var item in Leg2)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg4)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg1)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg10)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }

                             break;
                         }
                     }

                     break;    
                 }
                     case "Uranus":
                 {
                     switch (ToSearchString)
                     {
                         case "Jupiter":
                         {

                             int R = 0;
                             foreach (var item2 in Leg11)
                             {
                                 displayModel[R].Form = item2.From;
                                 displayModel[R].To = item2.To;
                                 displayModel[R].FlightStart = item2.FlightStart;
                                 displayModel[R].FlightEnd = item2.FlightEnd;
                                 displayModel[R].PriceListId = item2.PriceListDomainId;
                                 displayModel[R].CompanyName = item2.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;
                             }
                             foreach (var item2 in Leg13)
                             {
                                 displayModel[R].Form = item2.From;
                                 displayModel[R].To = item2.To;
                                 displayModel[R].FlightStart = item2.FlightStart;
                                 displayModel[R].FlightEnd = item2.FlightEnd;
                                 displayModel[R].PriceListId = item2.PriceListDomainId;
                                 displayModel[R].CompanyName = item2.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;
                             }
                             foreach (var item in Leg2)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg4)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg0)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             break;
                         }
                         case "Mars":
                         {
                             int R = 0;
                             foreach (var item2 in Leg11)
                             {
                                 displayModel[R].Form = item2.From;
                                 displayModel[R].To = item2.To;
                                 displayModel[R].FlightStart = item2.FlightStart;
                                 displayModel[R].FlightEnd = item2.FlightEnd;
                                 displayModel[R].PriceListId = item2.PriceListDomainId;
                                 displayModel[R].CompanyName = item2.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;
                             }
                             foreach (var item2 in Leg13)
                             {
                                 displayModel[R].Form = item2.From;
                                 displayModel[R].To = item2.To;
                                 displayModel[R].FlightStart = item2.FlightStart;
                                 displayModel[R].FlightEnd = item2.FlightEnd;
                                 displayModel[R].PriceListId = item2.PriceListDomainId;
                                 displayModel[R].CompanyName = item2.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;
                             }
                             foreach (var item in Leg2)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg4)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg0)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg6)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }

                             break;
                         }
                         case "Earth":
                         {
                             int R = 0;
                             foreach (var item2 in Leg11)
                             {
                                 displayModel[R].Form = item2.From;
                                 displayModel[R].To = item2.To;
                                 displayModel[R].FlightStart = item2.FlightStart;
                                 displayModel[R].FlightEnd = item2.FlightEnd;
                                 displayModel[R].PriceListId = item2.PriceListDomainId;
                                 displayModel[R].CompanyName = item2.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;
                             }
                             foreach (var item2 in Leg13)
                             {
                                 displayModel[R].Form = item2.From;
                                 displayModel[R].To = item2.To;
                                 displayModel[R].FlightStart = item2.FlightStart;
                                 displayModel[R].FlightEnd = item2.FlightEnd;
                                 displayModel[R].PriceListId = item2.PriceListDomainId;
                                 displayModel[R].CompanyName = item2.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;
                             }
                             foreach (var item in Leg2)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             foreach (var item in Leg4)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             break;
                         }
                         case "Venus":
                         {
                             int R = 0;
                             foreach (var item2 in Leg11)
                             {
                                 displayModel[R].Form = item2.From;
                                 displayModel[R].To = item2.To;
                                 displayModel[R].FlightStart = item2.FlightStart;
                                 displayModel[R].FlightEnd = item2.FlightEnd;
                                 displayModel[R].PriceListId = item2.PriceListDomainId;
                                 displayModel[R].CompanyName = item2.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;
                             }
                             foreach (var item2 in Leg13)
                             {
                                 displayModel[R].Form = item2.From;
                                 displayModel[R].To = item2.To;
                                 displayModel[R].FlightStart = item2.FlightStart;
                                 displayModel[R].FlightEnd = item2.FlightEnd;
                                 displayModel[R].PriceListId = item2.PriceListDomainId;
                                 displayModel[R].CompanyName = item2.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;
                             }
                             foreach (var item in Leg2)
                             {

                                 displayModel[R].Form = item.From;
                                 displayModel[R].To = item.To;
                                 displayModel[R].FlightStart = item.FlightStart;
                                 displayModel[R].FlightEnd = item.FlightEnd;
                                 displayModel[R].PriceListId = item.PriceListDomainId;
                                 displayModel[R].CompanyName = item.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;

                             }
                             break;
                         }
                         case "Mercury":
                         {
                             int R = 0;
                             foreach (var item2 in Leg11)
                             {
                                 displayModel[R].Form = item2.From;
                                 displayModel[R].To = item2.To;
                                 displayModel[R].FlightStart = item2.FlightStart;
                                 displayModel[R].FlightEnd = item2.FlightEnd;
                                 displayModel[R].PriceListId = item2.PriceListDomainId;
                                 displayModel[R].CompanyName = item2.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;
                             }
                             foreach (var item2 in Leg13)
                             {
                                 displayModel[R].Form = item2.From;
                                 displayModel[R].To = item2.To;
                                 displayModel[R].FlightStart = item2.FlightStart;
                                 displayModel[R].FlightEnd = item2.FlightEnd;
                                 displayModel[R].PriceListId = item2.PriceListDomainId;
                                 displayModel[R].CompanyName = item2.CompanyName;
                                 displayModel[R].TravelTime = displayModel[R].FlightEnd.Subtract(displayModel[R].FlightStart);
                                 R++;
                             }
                             break;
                         }
                     }
                     break;
                 }
                 case "Saturn":
                 {
                         switch (ToSearchString)
                         {
                                 case "Uranus":
                                 {

                                     break;
                                 }

                         }


                         break;
                 }










             }      

             */




        }

        public async Task<List<DisplayModel>> GetSpaceTravelDataDomain()
        {
    

           
            var PriceListId = await _context.IdAi.OrderByDescending(e => e.Id).FirstOrDefaultAsync();
            var Data = _context.ProviderAllDomains.Where(e => e.PriceListDomainId.Contains(PriceListId.PriceListDomainId));
            int NumberOfMachingData = _context.ProviderAllDomains.Where(e => e.PriceListDomainId.Contains(PriceListId.PriceListDomainId)).Count();
            // IQueryable<LegDomain> query = _context.LegDomains;
            //  query.OrderByDescending(LegDomain.);
            List<DisplayModel> displayModel = new List<DisplayModel>();
         
            if (PriceListId.PriceListDomainId != null)
            {              
                foreach (var item in Data)
                {
                    displayModel.Add(new DisplayModel
                    {
                        ProviderId = item.ProviderId,
                        Form = item.From,
                        To = item.To,
                        FlightStart =item.FlightStart,
                        FlightEnd = item.FlightEnd,
                        PriceListId = item.PriceListDomainId,
                        CompanyName = item.CompanyName,
                        Price = item.Price,
                        TravelTime = item.FlightEnd.Subtract(item.FlightStart)
                    });
                    
                        


                        /*displayModel[i].ProviderId = item.ProviderId;
                        displayModel[i].Form = item.From;
                        displayModel[i].To = item.To;
                        displayModel[i].FlightStart = item.FlightStart;
                        displayModel[i].FlightEnd = item.FlightEnd;
                        displayModel[i].PriceListId = item.PriceListDomainId;
                        displayModel[i].CompanyName = item.CompanyName;
                        displayModel[i].TravelTime = displayModel[i].FlightEnd.Subtract(displayModel[i].FlightStart);
                        i++;*/
                 
          
                }
              //  displayModel[0] = (DisplayModel)_context.ProviderAllDomains.Where(e => e.PriceListDomainId.Contains(stuffid.PriceListDomainId))
              // displayModel.CompanyName = c 


                //query = query.Where(e => e.Id.Contains(stuffid.PriceListDomainId));
                //     .Include(e => e.ProvidersDomain)
                //   .ThenInclude(e => e.CompanyDomain);

            }
            // List<LegDomain> leg = query.ToList();
            return displayModel;
            //await query.ToListAsync();
        }
        
        public async void PostRegData(RegistrationModel regModel)
        {
            RegistrationModelDomain domainReg = new RegistrationModelDomain();

            domainReg.Companys = regModel.Companys;
            domainReg.FirstName = regModel.FirstName;
            domainReg.LastName = regModel.LastName;
            domainReg.Routes = regModel.Routes;
            domainReg.TotalPrice = regModel.TotalPrice;
            domainReg.TotalTravelTime = regModel.TotalTravelTime;
            domainReg.PriceListDomainId = regModel.PriceListId;
            _context.RegistrationModelDomain.Add(domainReg);
             await _context.SaveChangesAsync();
        }
  








        
       
        
































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
