using ECommerceMVC.Core.Models.Response;
using System.Collections.Generic;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface IFavoriteService
    {
        public void AddToFavorites(int customerId, int productId);
        public bool FavoriteExists(int customerId, int productId);
        public List<ProductResponseModel> GetFavoritesByCustomer(int customerId); // artık ProductResponseModel dönüyor
        public void RemoveFromFavorites(int customerId, int productId);
        public void ClearFavorites(int customerId);
    }
}
