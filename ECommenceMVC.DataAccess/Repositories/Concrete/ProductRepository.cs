using ECommenceMVC.DataAccess.Queries;
using ECommenceMVC.DataAccess.Repositories.Abstract;
using ECommenceMVC.Entities.Models;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ECommenceMVC.DataAccess.Repositories.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Product> GetAllProduct()
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Product>(ProductSqlQueries.GetAllProduct).ToList();
        }
        public List<Product> GetProductsByCategory(int categoryId)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Product>(ProductSqlQueries.GetProductsByCategory, new { CategoryID = categoryId }).ToList();
        }

        public List<Product> GetTop5BestSellingProducts()
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Product>(ProductSqlQueries.GetTop5BestSellingProducts).ToList();
        }
       
    }
}
