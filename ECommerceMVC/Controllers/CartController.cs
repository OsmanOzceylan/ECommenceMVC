using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.Web.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cartItems = CartHelper.GetCartItems(HttpContext);
            return View(cartItems);
        }
        [HttpPost]
        public IActionResult AddToCart(int productId, string productName, decimal unitPrice)
        {
            var cartItems = CartHelper.GetCartItems(HttpContext);

            var existingItem = cartItems.FirstOrDefault(x => x.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                cartItems.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = productName,
                    Quantity = 1,
                    UnitPrice = unitPrice
                });
            }

            CartHelper.SaveCartItems(HttpContext, cartItems);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult IncreaseQuantity(int productId)
        {
            CartHelper.IncreaseQuantity(HttpContext, productId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DecreaseQuantity(int productId)
        {
            CartHelper.DecreaseQuantity(HttpContext, productId);

            return RedirectToAction("Index");
        }
        public IActionResult ClearCart()
        {
            CartHelper.ClearCart(HttpContext);
            return RedirectToAction("Index");
        }
    }
}
