using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECommerceMVC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var topProducts = await _productService.GetTop5BestSellingProductsAsync();
            return View(topProducts);
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
