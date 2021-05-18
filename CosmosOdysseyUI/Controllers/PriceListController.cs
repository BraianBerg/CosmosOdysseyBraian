using CosmosOdyssey.Core.Models;
using CosmosOdyssey.Data;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using CosmosOdyssey.Core.Domain;

namespace CosmosOdysseyUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceListController : Controller
    {

        private readonly CosmosOdysseyDbContext _context;
        private readonly IHttpClientFactory _clientFactory;

        public PriceListController(IHttpClientFactory clientFactory, CosmosOdysseyDbContext context)
        {
            _clientFactory = clientFactory;
            _context = context;
        }
        //Task<ActionResult<PriceListDomain>>
        //with ajax
        [HttpPost]
        public async void PostRootData()
        {
    
            var client = _clientFactory.CreateClient("meta");
            Rootobject rootobject = new Rootobject();
            PriceListDomain priceList = new PriceListDomain();
            rootobject = await client.GetFromJsonAsync<Rootobject>("v1.0/TravelPrices");
            priceList.Id = rootobject.Id;
            priceList.ValidUntil = rootobject.ValidUntil;
           
         /*   priceList.LegsDomain[0].Id = rootobject.Legs[0].Id;
             priceList.LegsDomain[0].RouteInfoDomain.Id = rootobject.Legs[0].RouteInfo.Id;
            priceList.LegsDomain[0].RouteInfoDomain.Distance = rootobject.Legs[0].RouteInfo.Distance;
            priceList.LegsDomain[0].RouteInfoDomain.FromDomain.Id = rootobject.Legs[0].RouteInfo.From.Id;
            priceList.LegsDomain[0].RouteInfoDomain.FromDomain.Name = rootobject.Legs[0].RouteInfo.From.Name;
            priceList.LegsDomain[0].RouteInfoDomain.ToDomain.Id = rootobject.Legs[0].RouteInfo.To.Id;
            priceList.LegsDomain[0].RouteInfoDomain.ToDomain.Name = rootobject.Legs[0].RouteInfo.To.Name;
            priceList.LegsDomain[0].ProvidersDomain[0].CompanyDomain.Id = rootobject.Legs[0].Providers[0].Company.Id;
            priceList.LegsDomain[0].ProvidersDomain[0].CompanyDomain.Name = rootobject.Legs[0].Providers[0].Company.Name;
            priceList.LegsDomain[0].ProvidersDomain[0].Id = rootobject.Legs[0].Providers[0].Id;
            priceList.LegsDomain[0].ProvidersDomain[0].Price = rootobject.Legs[0].Providers[0].Price;
            priceList.LegsDomain[0].ProvidersDomain[0].FlightStart = rootobject.Legs[0].Providers[0].FlightStart;
            priceList.LegsDomain[0].ProvidersDomain[0].FlightEnd = rootobject.Legs[0].Providers[0].FlightEnd;*/
            _context.PriceListDomains.Add(priceList);
            await _context.SaveChangesAsync();
          /*  try
            {
          
            }
            catch (Exception)
            {

                throw;
            }*/
           // return CreatedAtAction("GetSpaceTravelDataDomain", new {  });
        
        }

        //[HttpPost]
      /*/  public IActionResult Postdata()
        {

            if (ModelState.IsValid)
            {

            }
            return;
        }*/




    }
}
