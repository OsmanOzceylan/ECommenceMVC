using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequestModel model)
        {
            var user = _userRepository.GetUserInformation(model.UserName, model.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserName", user.UserName);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid login.";
            return View(model);
        }
    }
}
