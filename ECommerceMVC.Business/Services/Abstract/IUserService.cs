using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.Entities.Models;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface IUserService
    {
        User? GetUserInformation(string userName, string password);
        User? GetUserByUserName(string userName);
        void CreateUser(User user); //dönmeyeceği için 
        bool RegisterUser(RegisterRequest model); // 1 ya da 0 döneceği için


    }
}
