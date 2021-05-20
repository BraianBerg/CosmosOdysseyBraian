using CosmosOdyssey.Core.Domain;
using CosmosOdyssey.Core.Models;
using CosmosOdyssey.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmosOdyssey.ApplicationServices
{
    public interface IPriceListServices
    {

        Task<Rootobject> GetDataFromJson();
        Task<List<DisplayModel>> GetSpaceTravelDataDomain();
        Task<List<DisplayModel>> SearchForFlight(string FromSearchStirng, string ToSearchString, string? Company);
        void PostRootData();
        void PostRegData(RegistrationModel regModel);
        void ChekAndDeleteOldPriceList();
        Task<DateTime> GetValidUntil();
        Task<List<RegistrationModel>> GetRegisteredPeople();
    }
}