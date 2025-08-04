using ECommerceMVC.Entities.Models;

namespace ECommerceMVC.DataAccess.Repositories.Abstract
{
    public interface IUserRepository
    {
        User? GetUserByUserName(string userName);
        User? GetUserInformation(string serName, string Password);
        void CreateUser(User user);
    }
}