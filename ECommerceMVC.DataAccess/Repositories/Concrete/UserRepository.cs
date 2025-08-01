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
        public User? GetUserInformation(string userName, string password)
        {
            using var connection = new SqlConnection(_connectionString);
            var user = connection.QueryFirst<User>(
                UserQueries.GetUserInfo,
                new { UserName = new DbString { Value = userName, Length = 50, IsFixedLength = false, IsAnsi = false }, Password = password }
            );
            return user;

        }
    }
}
