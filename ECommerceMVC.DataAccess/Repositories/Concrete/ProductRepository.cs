using Dapper;
using ECommerceMVC.Core.Models.Response;
using ECommerceMVC.Core.Utilities;
using ECommerceMVC.DataAccess.Queries;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
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
        public Result<bool> BulkInsertProducts(List<Product> products)
        {
            using var connection = new SqlConnection(_connectionString);

            connection.Open();
            connection.Execute(ProductSqlQueries.CreateTempProductTable); //çalıştır
            DataTable dt = new DataTable();

            //Sütun ekleme
            dt.Columns.Add("ProductID", typeof(int)).AutoIncrement = true; //Identity mantığı
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("CategoryID", typeof(int));
            dt.Columns.Add("UnitPrice", typeof(decimal));
            dt.Columns.Add("UnitsInStock", typeof(int));
            dt.Columns.Add("ImageUrl", typeof(string));



            //for ile data table'a aktarma
            for (int i = 0; i < products.Count; i++)
            {
                DataRow row = dt.NewRow();
                row["ProductID"] = products[i].ProductID;
                row["ProductName"] = products[i].ProductName;
                row["CategoryID"] = products[i].CategoryID;
                row["UnitPrice"] = products[i].UnitPrice;
                row["UnitsInStock"] = products[i].UnitsInStock;
                row["ImageUrl"] = products[i].ImageUrl;
                dt.Rows.Add(row);
                using (SqlBulkCopy sqlBulkCopy = new(connection))
                {
                    sqlBulkCopy.DestinationTableName = "#TempProducts";
                    sqlBulkCopy.BulkCopyTimeout = 0;
                    dt.Columns
                        .Cast<DataColumn>()
                        .ToList()
                        .ForEach(column => sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName));   // her kolon için eşleştirme
                    sqlBulkCopy.WriteToServer(dt);
                }
            }
            return new Result<bool>()
            {
                Success = true,
                Message = "Ürünler başarıyla eklendi.",
                Data = true
            };
        }
        public async Task<ProductResponseModel> GetProductByIdAsync(int productId)
        {
            using var connection = new SqlConnection(_connectionString);
            var product = await connection.QueryFirstOrDefaultAsync<Product>(
                ProductSqlQueries.GetProductById, new { ProductID = productId });

            if (product == null) return null;

            return new ProductResponseModel
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                CategoryName = product.CategoryName,
                UnitPrice = product.UnitPrice,
                ImageUrl = product.ImageUrl,
                Quantity = 1,       
                TotalSold = product.TotalSold
            };
        }

    }
}


