using Dapper;
using ECommerceMVC.DataAccess.Queries;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;
using ECommerceMVC.Core.Utilities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ECommerceMVC.DataAccess.Repositories.Concrete
{
    public class CustomerProfileRepository : ICustomerProfileRepository
    {
        private readonly string _connectionString;

        public CustomerProfileRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // CustomerId'ye göre profil getir
        public Result<CustomerProfile> GetProfileByCustomerId(int customerId)
        {
            using var connection = new SqlConnection(_connectionString);
            var profile = connection.QueryFirstOrDefault<CustomerProfile>(
                CustomerProfileQueries.GetCustomerProfileByCustomerId,
                new { CustomerId = customerId }
            );

            if (profile != null)
                return Result<CustomerProfile>.Ok(profile, "Profil bulundu.");
            else
                return Result<CustomerProfile>.Fail("Profil bulunamadı.");
        }

        // Yeni profil oluştur
        public Result<string> CreateProfile(CustomerProfile profile)
        {
            using var connection = new SqlConnection(_connectionString);
            int rowsAffected = connection.Execute(CustomerProfileQueries.InsertCustomerProfile,
                new
                {
                    CustomerId = profile.CustomerId,
                    FullName = profile.FullName,
                    Address = profile.Address,
                    City = profile.City,
                    PostalCode = profile.PostalCode,
                    PhoneNumber = profile.PhoneNumber
                });

            if (rowsAffected > 0)
                return Result<string>.Ok("Profil başarıyla oluşturuldu.");
            else
                return Result<string>.Fail("Profil oluşturulamadı.");
        }

        // Mevcut profili güncelle
        public Result<string> UpdateProfile(CustomerProfile profile)
        {
            using var connection = new SqlConnection(_connectionString);
            int rowsAffected = connection.Execute(CustomerProfileQueries.UpdateCustomerProfile,
                new
                {
                    CustomerId = profile.CustomerId,
                    FullName = profile.FullName,
                    Address = profile.Address,
                    City = profile.City,
                    PostalCode = profile.PostalCode,
                    PhoneNumber = profile.PhoneNumber
                });

            if (rowsAffected > 0)
                return Result<string>.Ok("Profil başarıyla güncellendi.");
            else
                return Result<string>.Fail("Profil güncellenemedi.");
        }
    }
}
