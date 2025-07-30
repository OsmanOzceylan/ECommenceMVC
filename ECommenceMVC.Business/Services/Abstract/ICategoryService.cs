using ECommenceMVC.Entities.Models;

namespace ECommenceMVC.Business.Services.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
    }
}
