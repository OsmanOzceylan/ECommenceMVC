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
        private readonly ICustomerProfileService _profileService; 

        public OrderController(IOrderService orderService, ICartService cartService,ICustomerProfileService profileService) 
        {
            _orderService = orderService;
            _cartService = cartService;
            _profileService = profileService;
        }

        [HttpGet]

        [HttpGet]
        public IActionResult Checkout()
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerID");
            CheckoutRequest model = new CheckoutRequest();

            if (customerId != null)
            {
                var profileResult = _profileService.GetProfileByCustomerId(customerId.Value);
                if (profileResult.Success)
                {
                    // CheckoutRequest ile CustomerProfile eşleştir
                    model.FirstName = profileResult.Data.FullName;
                    model.LastName = profileResult.Data.FullName;
                    model.Address = profileResult.Data.Address;
                    model.City = profileResult.Data.City;
                    model.PostalCode = profileResult.Data.PostalCode;
                    model.PhoneNumber = profileResult.Data.PhoneNumber;
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

            // Giriş yapan kullanıcının ID'sini session'dan al
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
