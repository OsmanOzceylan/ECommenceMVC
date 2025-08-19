using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceMVC.Core.Models.Response
{
    public class HomeIndexViewModel
    {
        public List<ProductResponseModel> TopSellingProducts { get; set; }
        public List<ProductResponseModel> FavoriteProducts { get; set; }
        public List<ProductResponseModel> AllProducts { get; set; }
    }
}