using ECommenceMVC.Business.Services.Abstract;
using ECommenceMVC.Business.Services.Concrete;
using ECommenceMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace silinecek.Controllers
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


        public IActionResult Index()
        {
            var topProducts = _productService.GetTop5BestSellingProducts();
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
