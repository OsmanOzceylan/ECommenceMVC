using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ECommerceMVC.Business.Services.Concrete
{
    public class CartService : ICartService
    {
        private const string CartSessionKey = "CartSession";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        public List<CartItem> GetCartItems()
        {
            var sessionData = _session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(sessionData))
                return new List<CartItem>();

            return JsonConvert.DeserializeObject<List<CartItem>>(sessionData);
        }

        public void SaveCartItems(List<CartItem> cartItems)
        {
            var sessionData = JsonConvert.SerializeObject(cartItems);
            _session.SetString(CartSessionKey, sessionData);
        }

        public List<CartItem> AddToCart(List<CartItem> cartItems, CartItem newItem)
        {
            var existingItem = cartItems.FirstOrDefault(x => x.ProductId == newItem.ProductId);
            if (existingItem != null)
                existingItem.Quantity += newItem.Quantity;
            else
                cartItems.Add(newItem);

            SaveCartItems(cartItems);
            return cartItems;
        }

        public List<CartItem> IncreaseQuantity(List<CartItem> cartItems, int productId)
        {
            var item = cartItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
                item.Quantity++;

            SaveCartItems(cartItems);
            return cartItems;
        }

        public List<CartItem> DecreaseQuantity(List<CartItem> cartItems, int productId)
        {
            var item = cartItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                    cartItems.Remove(item);
            }

            SaveCartItems(cartItems);
            return cartItems;
        }

        public List<CartItem> ClearCart()
        {
            var emptyList = new List<CartItem>();
            SaveCartItems(emptyList);
            return emptyList;
        }
    }
}
