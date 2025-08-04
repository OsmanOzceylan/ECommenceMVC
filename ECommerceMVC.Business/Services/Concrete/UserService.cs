using ECommerceMVC.Business.Helper;
using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
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
        public bool RegisterUser(RegisterRequest model)
        {
            var existingUser = _userRepository.GetUserByUserName(model.UserName);
            if (existingUser != null)
                return false;
            var newUser = new User
            {
                UserName = model.UserName,
                Password = PasswordHelper.HashPassword(model.Password)
            };
            _userRepository.CreateUser(newUser);
            return true;
        }
    }
}