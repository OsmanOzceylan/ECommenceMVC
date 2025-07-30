using ECommenceMVC.Entities.Models;


namespace ECommenceMVC.DataAccess.Repositories.Abstract
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
    }
}
