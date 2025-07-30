using ECommenceMVC.Business.Services.Abstract;
using ECommenceMVC.Core.Models.Response;
using ECommenceMVC.DataAccess.Repositories.Abstract;
namespace ECommenceMVC.Business.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<ProductResponse> GetAllProducts()
        {
            var products = _productRepository.GetAllProduct();

            var responseList = products.Select(p => new ProductResponse
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                CategoryName = p.CategoryName, 
                UnitPrice = p.UnitPrice
            }).ToList();

            return responseList;
        }
        public List<ProductResponse> GetProductsByCategory(int categoryId)
        {
            var products = _productRepository.GetProductsByCategory(categoryId);

            var responseList = products.Select(p => new ProductResponse
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                CategoryName = p.CategoryName,
                UnitPrice = p.UnitPrice
            }).ToList();

            return responseList;
        }
        public List<ProductResponse> GetTop5BestSellingProducts()
        {
            var products = _productRepository.GetTop5BestSellingProducts();

            var responseList = products.Select(p => new ProductResponse
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                CategoryName = p.CategoryName,
                UnitPrice = p.UnitPrice,
                TotalSold = p.TotalSold
            }).ToList();

            return responseList;
        }

    }
}
