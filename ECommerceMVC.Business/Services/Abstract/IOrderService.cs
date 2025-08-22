using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.Entities.Models;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface IOrderService
    {
        // Checkout sayfası için model hazırla (0 iş controller)
        Task<CheckoutRequest> GetCheckoutRequestAsync(int? customerId);

        // Sepet ve checkout ile siparişi işle
        Task<(bool Success, string Message)> ProcessCheckoutAsync(int? customerId, CheckoutRequest checkout);
    }
}
