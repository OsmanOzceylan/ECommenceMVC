using ECommerceMVC.Entities.Models;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
    }
}
