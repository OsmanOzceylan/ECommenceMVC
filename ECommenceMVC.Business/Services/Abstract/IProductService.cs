using ECommenceMVC.Core.Models.Response;

namespace ECommenceMVC.Business.Services.Abstract
{
    public interface IProductService
    {
        List<ProductResponse> GetAllProducts();
        List<ProductResponse> GetProductsByCategory(int categoryid);

        List<ProductResponse> GetTop5BestSellingProducts();

    }

}
