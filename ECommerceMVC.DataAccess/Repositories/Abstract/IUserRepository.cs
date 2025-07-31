using ECommerceMVC.Entities.Models;

namespace ECommerceMVC.DataAccess.Repositories.Abstract
{
    public interface IUserRepository
    {
        User? GetUserInformation(string UserName, string Password);
    }
}
