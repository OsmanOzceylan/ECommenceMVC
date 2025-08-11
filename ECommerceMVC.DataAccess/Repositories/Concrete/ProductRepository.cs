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
            var productDT = ConvertToDataTable(products);
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
            }
        }

        private DataTable ConvertToDataTable<T>(List<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name); //DataTable oluşturma, ve bunu T olarak isimlendirme
            var props = typeof(T).GetProperties(); // T nin propertylerini alma

            foreach (var prop in props) 
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); //Null çin kontrol etme ve DataTable'a kolon ekleme
            }

            foreach (var item in items) // her item için satır ekleme
            {
                var values = new object[props.Length];      //her kolon için değerleri tutacak bir dizi
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values); // satırı dataTable'a ekleme
            }

            return dataTable;
        }
    }
}


