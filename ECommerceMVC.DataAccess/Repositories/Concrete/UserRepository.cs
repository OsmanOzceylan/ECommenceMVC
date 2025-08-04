using Dapper;
using ECommerceMVC.DataAccess.Queries;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ECommerceMVC.DataAccess.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public User? GetUserByUserName(string userName)
        {
            using var connection = new SqlConnection(_connectionString);
            var user = connection.QueryFirstOrDefault<User>(UserQueries.GetUserByUserName,
                new { UserName = new DbString { Value = userName, Length = 50, IsFixedLength = false, IsAnsi = false } }
        );
            return user;
        }
        public User? GetUserInformation(string userName, string password)
        {
            using var connection = new SqlConnection(_connectionString);
            var user = connection.QueryFirstOrDefault<User>(
                UserQueries.GetUserInfo,
                new { UserName = new DbString { Value = userName, Length = 50, IsFixedLength = false, IsAnsi = false }, Password = password }
            );
            return user;
        }
        public void CreateUser(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute(UserQueries.CreateUser,
            new { UserName = new DbString { Value = user.UserName, Length = 50, IsFixedLength = false, IsAnsi = false }, Password = user.Password }
        );
        }
    }
}