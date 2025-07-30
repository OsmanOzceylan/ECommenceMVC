using ECommenceMVC.Entities.Models;
using Dapper;
namespace ECommenceMVC.DataAccess.Repositories.Abstract
{
    public interface IProductRepository
    {
        List<Product> GetAllProduct();
        List<Product> GetProductsByCategory(int categoryId);
        List<Product> GetTop5BestSellingProducts();
        
    }
}
