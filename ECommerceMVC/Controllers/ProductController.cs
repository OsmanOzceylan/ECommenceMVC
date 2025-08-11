using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Response;
using ECommerceMVC.Entities.Models;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        [HttpPost]
        public IActionResult AddToCart(int productId, string productName, decimal unitPrice)
        {
            _cartService.AddToCart(productId, productName, unitPrice);
            TempData["SuccessMessage"] = $"{productName} sepete eklendi.";
            return RedirectToAction("Index");
        }
        public IActionResult ImportProducts([FromBody] List<Product> products)
        {
            if (products == null || products.Count == 0)
                return BadRequest("Gönderilen ürün listesi boş olamaz");
            var result = _productService.BulkInsertProducts(products);
            if (result)
            {
                TempData["SuccessMessage"] = "Ürünler başarıyla eklendi.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Ürünler eklenirken bir hata oluştu.";
                return RedirectToAction("Index");
            }
        }
    }
}
