using ECommerceMVC.Entities.Models;


namespace ECommerceMVC.DataAccess.Repositories.Abstract
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
    }
}
