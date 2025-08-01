using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.Core.Models.Response;
using ECommerceMVC.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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
        [HttpPost]
        public IActionResult AddToCart(int productId, string productName, decimal unitPrice)
        {
            var cartItems = CartHelper.GetCartItems(HttpContext);

            var existingItem = cartItems.FirstOrDefault(x => x.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cartItems.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = productName,
                    Quantity = 1,
                    UnitPrice = unitPrice
                });
            
            }
            CartHelper.SaveCartItems(HttpContext, cartItems);
            TempData["SuccessMessage"] = $"{productName} sepete eklendi.";
            return RedirectToAction("Index");
        }

    }
}
