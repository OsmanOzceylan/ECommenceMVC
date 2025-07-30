using ECommerceMVC.Entities.Models;
using Dapper;
namespace ECommerceMVC.DataAccess.Repositories.Abstract
{
    public interface IProductRepository
    {
        List<Product> GetAllProduct();
        List<Product> GetProductsByCategory(int categoryId);
        List<Product> GetTop5BestSellingProducts();
        List<Product> GetProductsByCategoryName(string categoryName);
    }
}
