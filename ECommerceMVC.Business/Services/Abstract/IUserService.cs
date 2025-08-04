using ECommerceMVC.Entities.Models;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface IUserService
    {
        User? GetUserInformation(string userName, string password);
        User? GetUserByUserName(string userName);
        void CreateUser(User user);
    }
}
