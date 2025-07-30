using ECommerceMVC.DataAccess.Queries;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ECommerceMVC.DataAccess.Repositories.Concrete
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
        public List<Product> GetProductsByCategoryName(string categoryName) 
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Product>(
                ProductSqlQueries.GetProductsByCategoryName,
                new {
                    @CategoryName = new DbString
                    {
                        Value = categoryName,
                        IsFixedLength = false,
                        Length = 20,
                        IsAnsi = false
                    },
                }
                ).ToList();
        }
    }
}
