using ECommerceMVC.Core.Utilities;
using ECommerceMVC.Entities.Models;
namespace ECommerceMVC.DataAccess.Repositories.Abstract
{
    public interface ICustomerProfileRepository
    {
        Result<CustomerProfile> GetProfileByCustomerId(int customerId);
        Result<string> CreateProfile(CustomerProfile profile);
        Result<string> UpdateProfile(CustomerProfile profile);
    }


}
