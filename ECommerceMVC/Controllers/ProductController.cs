using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Response;
using ECommerceMVC.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ECommerceMVC.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICartService _cartService;

        public ProductController(IProductService productService, ICategoryService categoryService, ICartService cartService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _cartService = cartService;
        }

        // Ürün listesi (mevcut Index)
        public async Task<IActionResult> Index(int? categoryId, string categoryName)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories;

            List<ProductResponseModel> products;

            if (categoryId.HasValue)
                products = await _productService.GetProductsByCategoryAsync(categoryId.Value);
            else if (!string.IsNullOrEmpty(categoryName))
                products = await _productService.GetProductsByCategoryNameAsync(categoryName);
            else
                products = await _productService.GetAllProductsAsync();

            return View(products);
        }

        // Ürün detay sayfası
        public async Task<IActionResult> Details(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null) return NotFound();

            var favoriteCookie = Request.Cookies["FavoriteProducts"];
            var favorites = string.IsNullOrEmpty(favoriteCookie)
                ? new List<ProductResponseModel>()
                : JsonSerializer.Deserialize<List<ProductResponseModel>>(favoriteCookie);

            ViewBag.IsFavorite = favorites.Any(p => p.ProductID == productId);

            return View(product);
        }

        // Sepete ekleme
        [HttpPost]
        public IActionResult AddToCart(int productId, string productName, decimal unitPrice, string imageUrl)
        {
            if (HttpContext.Session.GetInt32("CustomerID") == null)
            {
                TempData["ErrorMessage"] = "Lütfen giriş yapın.";
                return RedirectToAction("CustomerLogin", "Account");
            }

            _cartService.AddToCart(productId, productName, unitPrice, imageUrl);
            TempData["SuccessMessage"] = $"{productName} sepete eklendi.";
            return RedirectToAction("Details", new { productId });
        }

        // Favori ekleme / çıkarma
        [HttpPost]
        public IActionResult ToggleFavorite(int productId, string productName, decimal unitPrice, string imageUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessage"] = "Lütfen giriş yapın.";
                return RedirectToAction("CustomerLogin", "Account");
            }

            var favoriteCookie = Request.Cookies["FavoriteProducts"];
            var favorites = string.IsNullOrEmpty(favoriteCookie)
                ? new List<ProductResponseModel>()
                : JsonSerializer.Deserialize<List<ProductResponseModel>>(favoriteCookie);

            var existing = favorites.FirstOrDefault(p => p.ProductID == productId);
            if (existing != null)
                favorites.Remove(existing);
            else
                favorites.Add(new ProductResponseModel
                {
                    ProductID = productId,
                    ProductName = productName,
                    UnitPrice = unitPrice,
                    ImageUrl = imageUrl
                });

            Response.Cookies.Append("FavoriteProducts", JsonSerializer.Serialize(favorites), new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            });

            return RedirectToAction("Details", new { productId });
        }
    }
}
