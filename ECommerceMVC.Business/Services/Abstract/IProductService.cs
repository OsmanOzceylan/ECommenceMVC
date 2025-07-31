using ECommerceMVC.Core.Models.Response;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface IProductService
    {
        Task<List<ProductResponseModel>> GetAllProductsAsync();
        Task<List<ProductResponseModel>> GetProductsByCategoryAsync(int categoryId);
        Task<List<ProductResponseModel>> GetProductsByCategoryNameAsync(string categoryName);
        Task<List<ProductResponseModel>> GetTop5BestSellingProductsAsync();
    }
}
