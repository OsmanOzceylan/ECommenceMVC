using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.Core.Utilities; // Result<T> için
using ECommerceMVC.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;

        public AccountController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // --- Customer Login ---
        [HttpGet]
        public IActionResult CustomerLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CustomerLogin(CustomerLoginRequestModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Result<Customer> result = _customerService.GetCustomerInformation(model.CustomerName, model.CustomerPassword);

            if (result.Success)
            {
                HttpContext.Session.SetString("CustomerName", result.Data.CustomerName);
                HttpContext.Session.SetInt32("CustomerID", result.Data.CustomerID);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = result.Message;
            return View(model);
        }

        // --- Customer Register ---
        [HttpGet]
        public IActionResult CustomerRegister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CustomerRegister(CustomerRegisterRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Result<string> result = _customerService.RegisterCustomer(model);

            if (result.Success)
                return RedirectToAction("CustomerLogin", "Account");

            ViewBag.Error = result.Message;
            return View(model);
        }

        // --- Customer Logout ---
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
