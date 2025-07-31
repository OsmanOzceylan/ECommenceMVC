using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Response;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> Index(int? categoryId, string categoryName)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories;

            List<ProductResponseModel> products;

            if (categoryId.HasValue)
            {
                products = await _productService.GetProductsByCategoryAsync(categoryId.Value);
            }
            else if (!string.IsNullOrEmpty(categoryName))
            {
                products = await _productService.GetProductsByCategoryNameAsync(categoryName);
            }
            else
            {
                products = await _productService.GetAllProductsAsync();
            }

            return View(products);
        }
    }
}
