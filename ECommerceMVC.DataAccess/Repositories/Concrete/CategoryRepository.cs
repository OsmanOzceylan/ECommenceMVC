using Dapper;
using ECommerceMVC.DataAccess.Queries;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ECommerceMVC.DataAccess.Repositories.Concrete
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
