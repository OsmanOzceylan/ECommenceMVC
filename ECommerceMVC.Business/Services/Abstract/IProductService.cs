using ECommerceMVC.Core.Models.Response;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface IProductService
    {
        List<ProductResponseModel> GetAllProducts();
        List<ProductResponseModel> GetProductsByCategory(int categoryid);

        List<ProductResponseModel> GetTop5BestSellingProducts();
        List<ProductResponseModel> GetProductsByCategoryName(string categoryName);
    }

}
