using ECommenceMVC.Business.Services.Abstract;
using ECommenceMVC.DataAccess.Repositories.Abstract;
using ECommenceMVC.Entities.Models;

namespace ECommenceMVC.Business.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories(); 
        }
    }
}
