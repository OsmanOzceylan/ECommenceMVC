using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.Web.Controllers
{
    [Route("Account/[action]")]
    public class CustomerProfileController : Controller
    {
        private readonly ICustomerProfileService _profileService;

        public CustomerProfileController(ICustomerProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            int customerId = (int)HttpContext.Session.GetInt32("CustomerID");
            var result = _profileService.GetProfileByCustomerId(customerId);

            if (!result.Success)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Profile(CustomerProfile model)
        {
            model.CustomerId = (int)HttpContext.Session.GetInt32("CustomerID");
            var result = _profileService.SaveProfile(model);

            if (!result.Success)
            {
                ViewBag.Error = result.Message;
                return View(model);
            }

            ViewBag.Success = result.Message;
            return View(model);
        }
    }
}
