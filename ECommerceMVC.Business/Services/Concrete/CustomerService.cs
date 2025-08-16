using ECommerceMVC.Business.Helper;
using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;
using ECommerceMVC.Core.Utilities; // Result<T> için

namespace ECommerceMVC.Business.Services.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Result<Customer> GetCustomerInformation(string customerName, string customerPassword)
        {
            var hashedPassword = PasswordHelper.HashPassword(customerPassword);
            var result = _customerRepository.GetCustomerInformation(customerName, hashedPassword);

            if (!result.Success || result.Data == null)
                return Result<Customer>.Fail("Geçersiz kullanıcı adı veya şifre.");

            return Result<Customer>.Ok(result.Data, result.Message);
        }


        public Result<string> CreateCustomer(Customer customer)
        {
            customer.CustomerPassword = PasswordHelper.HashPassword(customer.CustomerPassword);
            _customerRepository.CreateCustomer(customer);
            return Result<string>.Ok("Müşteri başarıyla oluşturuldu.");
        }

        public Result<Customer> GetCustomerByCustomerName(string customerName)
        {
            var customer = _customerRepository.GetCustomerByCustomerName(customerName);
            var result = _customerRepository.GetCustomerByCustomerName(customerName);

            if (customer == null)
                return Result<Customer>.Fail("Müşteri bulunamadı.");
            return Result<Customer>.Ok(result.Data, result.Message);
        }

        public Result<string> RegisterCustomer(CustomerRegisterRequest model)
        {
            var existingUser = _customerRepository.GetCustomerByCustomerName(model.CustomerName);
            if (existingUser != null)
                return Result<string>.Fail("Bu kullanıcı adı zaten mevcut.");

            var newCustomer = new Customer
            {
                CustomerName = model.CustomerName,
                CustomerPassword = PasswordHelper.HashPassword(model.CustomerPassword)
            };

            _customerRepository.CreateCustomer(newCustomer);
            return Result<string>.Ok("Kayıt işlemi başarıyla tamamlandı.");
        }
    }
}
