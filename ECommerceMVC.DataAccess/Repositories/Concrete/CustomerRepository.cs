using Dapper;
using ECommerceMVC.Core.Utilities;
using ECommerceMVC.DataAccess.Queries;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ECommerceMVC.DataAccess.Repositories.Concrete
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;
        public CustomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public Result<Customer> GetCustomerByCustomerName(string customerName)
        {
            using var connection = new SqlConnection(_connectionString);
            var customer = connection.QueryFirstOrDefault<Customer>(CustomerQueries.GetCustomerByCustomerName,
                new { CustomerName = new DbString { Value = customerName, Length = 50, IsFixedLength = false, IsAnsi = false } }
        );
            if (customer != null)
            {
                return Result<Customer>.Ok(customer, "Müşteri Bulundu.");
            }

            return Result<Customer>.Fail("Müşteri Bulunamadı.");
        }
        public Result<Customer> GetCustomerInformation(string customerName, string customerPassword)
        {
            using var connection = new SqlConnection(_connectionString);
            var customer = connection.QueryFirstOrDefault<Customer>(
                CustomerQueries.GetCustomerInfo,
                new { CustomerName = new DbString { Value = customerName, Length = 50, IsFixedLength = false, IsAnsi = false }, CustomerPassword = customerPassword }
            );
            if (customer != null)
            {
                return Result<Customer>.Ok(customer, "Müşteri bilgileri doğru.");
            }
            else
            {
                return Result<Customer>.Fail("Kullanıcı adı veya şifreniz hatalı.");
            }
        }
        public Result<string> CreateCustomer(Customer customer)
        {
            using var connection = new SqlConnection(_connectionString);
            int rowsAffected = connection.Execute(CustomerQueries.CreateCustomer,
            new { CustomerName = new DbString { Value = customer.CustomerName, Length = 50, IsFixedLength = false, IsAnsi = false }, CustomerPassword = customer.CustomerPassword }
        );
            if (rowsAffected > 0)
            {
                return Result<string>.Ok("Müşteri başarıyla oluşturuldu.");
            }
            else
            {
                return Result<string>.Fail("Müşteri oluşturulamadı.");
            }
        }
    }
}

