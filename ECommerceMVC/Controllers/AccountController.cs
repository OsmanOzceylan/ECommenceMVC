using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.Entities.Models;
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

            var existingUser = _userService.GetUserByUserName(model.UserName);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Bu kullanıcı adı zaten kayıtlı.");
                return View(model);
            }

            var newUser = new User
            {
                UserName = model.UserName,
                Password = model.Password // 
            };

            _userService.CreateUser(newUser);

            TempData["SuccessMessage"] = "Kayıt başarılı! Lütfen giriş yapınız.";
            return RedirectToAction("Login");
        }

    }
}
