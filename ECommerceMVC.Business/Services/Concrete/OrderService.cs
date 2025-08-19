using ECommerceMVC.Business.Services.Abstract;
using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.DataAccess.Repositories.Abstract;
using ECommerceMVC.Entities.Models;

namespace ECommerceMVC.Business.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartService _cartService;

        public OrderService(IOrderRepository orderRepository, ICartService cartService)
        {
            _orderRepository = orderRepository;
            _cartService = cartService;
        }

        public async Task<int> CreateOrderAsync(Order order, List<CartItem> cartItems, CheckoutRequest checkout)
        {     // Siparişi oluştur
            var orderId = await _orderRepository.CreateOrderAsync(order);

            // Sipariş detaylarını oluştur
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderID = orderId,
                    ProductID = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                };
                await _orderRepository.CreateOrderDetailAsync(orderDetail);
            }
            var orderInfo = new OrderInfo
            {
                OrderID = orderId,
                FirstName = checkout.FirstName,
                LastName = checkout.LastName,
                Email = checkout.Email,
                Address = checkout.Address,
                City = checkout.City,
                District = checkout.District,
                PostalCode = checkout.PostalCode,
                PhoneNumber = checkout.PhoneNumber,
            };
            await _orderRepository.CreateOrderInfoAsync(orderInfo);
            // Sepeti temizle
            _cartService.ClearCart();

            return orderId;
        }
    }
}
