using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceMVC.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly ICustomerService _profileService; // <-- burayı değiştiriyoruz

        public OrderController(IOrderService orderService, ICartService cartService, ICustomerService profileService)
        {
            _orderService = orderService;
            _cartService = cartService;
            _profileService = profileService;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerID");
            CheckoutRequest model = new CheckoutRequest();

            if (customerId != null)
            {
                var profileResult = _profileService.GetCustomerById(customerId.Value);
                if (profileResult.Success)
                {
                    // CheckoutRequest ile CustomerProfile eşleştir
                    model.FirstName = profileResult.Data.CustomerName;
                    model.LastName = profileResult.Data.CustomerLastName;
                    model.Email = profileResult.Data.Email;
                    model.Address = profileResult.Data.Address;
                    model.City = profileResult.Data.City;
                    model.Country = profileResult.Data.Country;
                    model.PostalCode = profileResult.Data.PostalCode;
                    model.PhoneNumber = profileResult.Data.Phone;
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var cartItems = _cartService.GetCartItems();

            if (cartItems.Count == 0)
            {
                TempData["ErrorMessage"] = "Sepetiniz boş, önce ürün ekleyin.";
                return RedirectToAction("Index", "Cart");
            }

            int? customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId == null)
            {
                TempData["ErrorMessage"] = "Lütfen önce giriş yapın.";
                return RedirectToAction("CustomerLogin", "Account");
            }

            var order = new Order
            {
                CustomerID = customerId.Value,
                OrderDate = DateTime.Now
            };

            var orderId = await _orderService.CreateOrderAsync(order, cartItems, model);

            TempData["SuccessMessage"] = $"Siparişiniz başarıyla oluşturuldu. Sipariş ID: {orderId}";
            return RedirectToAction("Index", "Product");
        }
    }
}
