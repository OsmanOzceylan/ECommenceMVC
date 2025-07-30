using ECommerceMVC.Core.Models.Response;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface IProductService
    {
        List<ProductResponse> GetAllProducts();
        List<ProductResponse> GetProductsByCategory(int categoryid);

        List<ProductResponse> GetTop5BestSellingProducts();
        List<ProductResponse> GetProductsByCategoryName(string categoryName);
    }

}
