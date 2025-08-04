using ECommerceMVC.Core.Models.Request;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface ICartService
    {
        List<CartItem> GetCartItems();
        void SaveCartItems(List<CartItem> cartItems);
        void AddToCart(int productId, string productName, decimal unitPrice);
        void IncreaseQuantity(int productId);
        List<CartItem> DecreaseQuantity(List<CartItem> cartItems, int productId);
        List<CartItem> ClearCart();
    }
}
