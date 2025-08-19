using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Response;
using ECommerceMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json; // Cookie için gerekli

namespace ECommerceMVC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            
            var topProducts = await _productService.GetTop5BestSellingProductsAsync();

            // 2️⃣ Cookie üzerinden favori ürünler
            var favoriteProducts = new List<ProductResponseModel>();
            var favoriteCookie = Request.Cookies["FavoriteProducts"];
            if (!string.IsNullOrEmpty(favoriteCookie))
            {
                favoriteProducts = JsonSerializer.Deserialize<List<ProductResponseModel>>(favoriteCookie);
            }           
            var allProducts = await _productService.GetAllProductsAsync();
            var categories = await _categoryService.GetAllCategoriesAsync();
            var viewModel = new HomeIndexViewModel
            {
                TopSellingProducts = topProducts,
                FavoriteProducts = favoriteProducts,
                AllProducts = allProducts
            };
            ViewBag.Categories = categories;
            return View(viewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
