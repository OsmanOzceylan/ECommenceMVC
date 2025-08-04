using ECommerceMVC.Business.Helper;
using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;


namespace ECommerceMVC.Business.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User? GetUserInformation(string userName, string password)
        {
            var hashedPasswords = PasswordHelper.HashPassword(password);
            return _userRepository.GetUserInformation(userName, hashedPasswords);
        }
        public void CreateUser(User user)
        {
            user.Password = PasswordHelper.HashPassword(user.Password);
            _userRepository.CreateUser(user);
        }
        public User? GetUserByUserName(string userName)
        {
            return _userRepository.GetUserByUserName(userName);
        }
    }
}
