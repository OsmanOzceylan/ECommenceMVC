using ECommerceMVC.Core.Models.Request;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface ICartService
    {
        List<CartItem> GetCartItems();
        void SaveCartItems(List<CartItem> cartItems);
        List<CartItem> AddToCart(List<CartItem> cartItems, CartItem newItem);
        List<CartItem> IncreaseQuantity(List<CartItem> cartItems, int productId);
        List<CartItem> DecreaseQuantity(List<CartItem> cartItems, int productId);
        List<CartItem> ClearCart();
    }
}
