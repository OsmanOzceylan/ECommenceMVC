using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, string productName, decimal unitPrice)
        {
            var cartItems = _cartService.GetCartItems();

            var newItem = new CartItem
            {
                ProductId = productId,
                ProductName = productName,
                Quantity = 1,
                UnitPrice = unitPrice
            };

            _cartService.AddToCart(cartItems, newItem);

            TempData["Message"] = $"{productName} sepete eklendi.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult IncreaseQuantity(int productId)
        {
            var cartItems = _cartService.GetCartItems();
            _cartService.IncreaseQuantity(cartItems, productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DecreaseQuantity(int productId)
        {
            var cartItems = _cartService.GetCartItems();
            _cartService.DecreaseQuantity(cartItems, productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return RedirectToAction("Index");
        }
    }
}
