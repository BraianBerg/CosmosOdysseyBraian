using CosmosOdyssey.Core.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmosOdysseyUI.UIservices
{
    public interface ICartService
    {
        Task DeleteItem(DisplayModel item);
        Task<List<DisplayModel>> GetCartItems();
    }
}