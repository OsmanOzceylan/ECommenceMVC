using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ECommerceMVC.Web.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        // Favoriler sayfası
        public IActionResult Index()
        {
            var customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId == null)
            {
                TempData["ErrorMessage"] = "Favorilere erişmek için giriş yapmalısınız.";
                return RedirectToAction("CustomerLogin", "Account");
            }

            var favorites = _favoriteService.GetFavoritesByCustomer(customerId.Value);
            return View(favorites);
        }

        // Favorilere ekle
        [HttpPost]
        public IActionResult AddToFavorites(int productId)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId == null)
            {
                TempData["ErrorMessage"] = "Favorilere eklemek için giriş yapmalısınız.";
                return RedirectToAction("CustomerLogin", "Account");
            }

            _favoriteService.AddToFavorites(customerId.Value, productId);
            TempData["SuccessMessage"] = "Ürün favorilere eklendi.";
            return RedirectToAction("Index");
        }

        // Favoriden kaldır
        [HttpPost]
        public IActionResult RemoveFromFavorites(int productId)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId != null)
            {
                _favoriteService.RemoveFromFavorites(customerId.Value, productId);
                TempData["SuccessMessage"] = "Ürün favorilerden silindi.";
            }
            return RedirectToAction("Index");
        }

        // Tüm favorileri temizle
        [HttpPost]
        public IActionResult ClearFavorites()
        {
            var customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId != null)
            {
                _favoriteService.ClearFavorites(customerId.Value);
                TempData["SuccessMessage"] = "Tüm favoriler temizlendi.";
            }
            return RedirectToAction("Index");
        }
    }
}
