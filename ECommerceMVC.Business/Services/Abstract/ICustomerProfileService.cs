using ECommerceMVC.Entities.Models;
using ECommerceMVC.Core.Utilities;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface ICustomerProfileService
    {
        Result<CustomerProfile> GetProfileByCustomerId(int customerId);
        Result<string> SaveProfile(CustomerProfile profile);
    }
}
