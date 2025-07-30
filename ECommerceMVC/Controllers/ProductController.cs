using ECommerceMVC.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using ECommerceMVC.Core.Models.Response;

namespace ECommerceMVC.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index(int? categoryId, string categoryName)
        {
            var categories = _categoryService.GetAllCategories();
            ViewBag.Categories = categories;

            List<ProductResponse> products;

            if (categoryId.HasValue)
            {
                products = _productService.GetProductsByCategory(categoryId.Value);
            }
            else if (!string.IsNullOrEmpty(categoryName)) 
            {
                products = _productService.GetProductsByCategoryName(categoryName);
            }
            else
            {
                products = _productService.GetAllProducts();
            }

            return View(products);
        }
    }
}
