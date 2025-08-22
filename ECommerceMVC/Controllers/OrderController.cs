using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerID");
            var model = await _orderService.GetCheckoutRequestAsync(customerId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutRequest model)
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerID");
            var result = await _orderService.ProcessCheckoutAsync(customerId, model);

            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Checkout");
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index", "Product");
        }
    }
}
