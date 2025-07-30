using Dapper;
using ECommenceMVC.DataAccess.Queries;
using ECommenceMVC.DataAccess.Repositories.Abstract;
using ECommenceMVC.Entities.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ECommenceMVC.DataAccess.Repositories.Concrete
{
    public class CategoryRepository : ICategoryRepository 
    {
        private readonly string _connectionString;

        public CategoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");       
        }

        public List<Category> GetAllCategories()
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Category>(CategorySqlQueries.GetAllCategories).ToList();
        }
    }
}
