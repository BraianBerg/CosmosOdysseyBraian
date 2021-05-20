using Blazored.LocalStorage;
using CosmosOdyssey.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosOdysseyUI.UIservices
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;

        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public async Task<List<DisplayModel>> GetCartItems()
        {

            var result = new List<DisplayModel>();
            var cart = await _localStorage.GetItemAsync<List<DisplayModel>>("cart");
            if (cart == null)
            {
                return result;
            }
            foreach (var item in cart)
            {
                var cartItem = (new DisplayModel{
                    PriceListId = item.PriceListId,
                    CompanyName = item.CompanyName,
                    ProviderId = item.ProviderId,
                    Form = item.Form,
                    To = item.To,
                    FlightStart = item.FlightStart,
                    FlightEnd = item.FlightEnd,
                    TravelTime = item.TravelTime,
                    Price = item.Price
                    
                   
                });
                result.Add(cartItem);
            }
            return result;
        }
        public async Task DeleteItem(DisplayModel item)
        {
            var cart = await _localStorage.GetItemAsync<List<DisplayModel>>("cart");
            if (cart == null )
            {
                return;

            }
            var cartitem = cart.Find(x => x.ProviderId == item.ProviderId);
            cart.Remove(cartitem);

            await _localStorage.SetItemAsync("cart", cart);
         
        }
    }
}
