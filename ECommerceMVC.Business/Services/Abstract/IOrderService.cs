using ECommerceMVC.Core.Models.Request;
using ECommerceMVC.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceMVC.Business.Services.Abstract
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(Order order, List<CartItem>cartItems, CheckoutRequest checkout); // List<CartItem> sepetteki ürünler
    }
}
