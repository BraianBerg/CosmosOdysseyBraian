using CosmosOdyssey.Core.Domain;
using CosmosOdyssey.Core.Models;
using CosmosOdyssey.Core.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmosOdyssey.ApplicationServices
{
    public interface IPriceListServices
    {
       // void DeleteOldPriceList(string id);
        Task<Rootobject> GetDataFromJson();
        Task<DisplayModel[]> GetSpaceTravelDataDomain();
        void PostRootData();
    }
}