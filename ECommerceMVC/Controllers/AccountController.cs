using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginRequestModel model)
        {
            var user = _userService.GetUserInformation(model.UserName, model.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserName", user.UserName);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid login.";
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = _userService.RegisterUser(model);
            if (!success)
            {
                ModelState.AddModelError("", "Bu kullanıcı adı zaten kayıtlı.");
                return View(model);
            }

            TempData["SuccessMessage"] = "Kayıt başarılı! Lütfen giriş yapınız.";
            return RedirectToAction("Login");
        }
    }
}
