using Dapper;
using ECommerceMVC.DataAccess.Queries;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.AccessControl;

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
        public bool BulkInsertProducts(List<Product> products)
        {
            using var connection = new SqlConnection(_connectionString);

            connection.Open();
            connection.Execute(ProductSqlQueries.CreateTempProductTable);
            var productDT = new DataTable("#TempProducts");
            using (SqlBulkCopy sqlBulkCopy = new(connection))
            {
                sqlBulkCopy.DestinationTableName = "#TempProducts";
                sqlBulkCopy.BulkCopyTimeout = 0;

                productDT.Columns
                    .Cast<DataColumn>()
                    .ToList()
                    .ForEach(column => sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName));   // her kolon için eşleştirme
                sqlBulkCopy.WriteToServer(productDT);
                return true;

                DataTable dt = new DataTable();

                //Sütun ekleme
                dt.Columns.Add("ProductID", typeof(int)).AutoIncrement = true; //otomatik arttırma
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("CategoryID", typeof(int));
                dt.Columns.Add("UnitPrice", typeof(decimal));
                dt.Columns.Add("UnitsInStock", typeof(int));

                //for ile data table'a aktarma
                for (int i = 0; i < products.Count; i++) 
                {
                    DataRow row = dt.NewRow();
                    row["ProductID"] = products[i].ProductID;
                    row["ProductName"] = products[i].ProductName;
                    row["CategoryID"] = products[i].CategoryID;
                    row["UnitPrice"] = products[i].UnitPrice;
                    row["UnitsInStock"] = products[i].UnitsInStock;
                    dt.Rows.Add(row);
                }
            }
        }
    }
}


