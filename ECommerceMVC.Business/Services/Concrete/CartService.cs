using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.Core.Utilities;
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
        public Result<string> AddToCart(int productId, string productName, decimal unitPrice, string? imageUrl)
        {
            try
            {
                var cartItems = GetCartItems();

                var existingItem = cartItems.FirstOrDefault(x => x.ProductId == productId);
                if (existingItem != null)
                {
                    existingItem.Quantity++;
                    SaveCartItems(cartItems);
                    return Result<string>.Ok(null, $"{productName} adedi 1 arttırıldı.");
                }
                else
                {
                    cartItems.Add(new CartItem
                    {
                        ProductId = productId,
                        ProductName = productName,
                        Quantity = 1,
                        UnitPrice = unitPrice,
                        ImageUrl = imageUrl
                    });
                    SaveCartItems(cartItems);
                    return Result<string>.Ok(null, $"{productName} sepete eklendi.");
                }
            }
            catch (Exception ex)
            {
                return Result<string>.Fail("Sepete eklenirken bir hata oluştu: " + ex.Message);
            }
        }
        public void IncreaseQuantity(int productId)
        {
            var cartItems = GetCartItems();
            var item = cartItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity++;
                SaveCartItems(cartItems);
            }
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
