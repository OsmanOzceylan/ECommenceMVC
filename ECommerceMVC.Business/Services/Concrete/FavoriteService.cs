using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Response;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceMVC.Business.Services.Concrete
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public void AddToFavorites(int customerId, int productId)
        {
            if (!_favoriteRepository.FavoriteExists(customerId, productId))
                _favoriteRepository.AddToFavorites(customerId, productId);
        }

        public bool FavoriteExists(int customerId, int productId)
        {
            return _favoriteRepository.FavoriteExists(customerId, productId);
        }

        public List<ProductResponseModel> GetFavoritesByCustomer(int customerId)
        {
            var favorites = _favoriteRepository.GetFavoritesByCustomer(customerId);

            return favorites.Select(f => new ProductResponseModel
            {
                ProductID = f.ProductID,
                ProductName = f.ProductName,
                UnitPrice = f.UnitPrice,
                ImageUrl = f.ImageUrl
            }).ToList();
        }

        public void RemoveFromFavorites(int customerId, int productId)
        {
            _favoriteRepository.RemoveFromFavorites(customerId, productId);
        }

        public void ClearFavorites(int customerId)
        {
            _favoriteRepository.ClearFavorites(customerId);
        }
    }
}
