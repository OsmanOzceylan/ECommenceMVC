using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Utilities;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;

namespace ECommerceMVC.Business.Services.Concrete
{
    public class CustomerProfileService : ICustomerProfileService
    {
        private readonly ICustomerProfileRepository _profileRepository;

        public CustomerProfileService(ICustomerProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        // CustomerId'ye göre profil bilgilerini getir
        public Result<CustomerProfile> GetProfileByCustomerId(int customerId)
        {
            var result = _profileRepository.GetProfileByCustomerId(customerId);

            if (!result.Success) // profil bulunamadıysa
                return Result<CustomerProfile>.Fail(result.Message);

            // profil bulunduysa Data ile geri dön
            return Result<CustomerProfile>.Ok(result.Data, result.Message);
        }

        // Profil kaydetme veya güncelleme
        public Result<string> SaveProfile(CustomerProfile profile)
        {
            var existingResult = _profileRepository.GetProfileByCustomerId(profile.CustomerId);

            if (!existingResult.Success) // profil yoksa, oluştur
            {
                return _profileRepository.CreateProfile(profile);
            }
            else // profil varsa, güncelle
            {
                return _profileRepository.UpdateProfile(profile);
            }
        }
    }
}
