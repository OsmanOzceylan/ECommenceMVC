using ECommerceMVC.Core.Models.Request;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceMVC.Web.Helpers
{
    public static class CartHelper
    {
        private const string CartSessionKey = "CartSession";

        public static List<CartItem> GetCartItems(HttpContext httpContext)
        {
            var sessionData = httpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(sessionData))
            {
                return new List<CartItem>();
            }

            return JsonConvert.DeserializeObject<List<CartItem>>(sessionData);
        }

        public static void SaveCartItems(HttpContext httpContext, List<CartItem> cartItems)
        {
            var sessionData = JsonConvert.SerializeObject(cartItems);
            httpContext.Session.SetString(CartSessionKey, sessionData);
        }

        public static void IncreaseQuantity(HttpContext httpContext, int productId)
        {
            var cartItems = GetCartItems(httpContext);
            var item = cartItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity++;
                SaveCartItems(httpContext, cartItems);
            }
        }


        public static void DecreaseQuantity(HttpContext httpContext, int productId)
        {
            var cartItems = GetCartItems(httpContext);
            var item = cartItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                {
                    cartItems.Remove(item);
                }
                SaveCartItems(httpContext, cartItems);
            }
        }

        public static void ClearCart(HttpContext httpContext)
        {
            httpContext.Session.Remove(CartSessionKey);
        }
    }
}
