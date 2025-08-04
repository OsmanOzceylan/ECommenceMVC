using Dapper;
using ECommerceMVC.DataAccess.Queries;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ECommerceMVC.DataAccess.Repositories.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<Product>> GetAllProduct()
        {
            using var connection = new SqlConnection(_connectionString);
            var products = await connection.QueryAsync<Product>(ProductSqlQueries.GetAllProduct);
            return products.ToList();
        }
        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            using var connection = new SqlConnection(_connectionString);
            var products = await connection.QueryAsync<Product>(ProductSqlQueries.GetProductsByCategory, new { CategoryID = categoryId });
            return products.ToList();
        }
        public async Task<List<Product>> GetTop5BestSellingProductsAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var products = await connection.QueryAsync<Product>(ProductSqlQueries.GetTop5BestSellingProducts);
            return products.ToList();
        }
        public async Task<List<Product>> GetProductsByCategoryNameAsync(string categoryName)
        {
            using var connection = new SqlConnection(_connectionString);
            var products = await connection.QueryAsync<Product>(ProductSqlQueries.GetProductsByCategoryName, new { CategoryName = categoryName });
            return products.ToList();
        }
    }
}

