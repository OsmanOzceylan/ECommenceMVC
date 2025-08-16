using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.Entities.Models;
using ECommerceMVC.Core.Utilities; // Result<T> için

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface ICustomerService
    {
        Result<Customer> GetCustomerInformation(string customerName, string customerPassword);
        Result<Customer> GetCustomerByCustomerName(string customerName);
        Result<string> CreateCustomer(Customer customer);
        Result<string> RegisterCustomer(CustomerRegisterRequest model);
    }
}
