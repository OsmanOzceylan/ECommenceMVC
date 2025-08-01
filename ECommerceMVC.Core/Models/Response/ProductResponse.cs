﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceMVC.Core.Models.Response
{
    public class ProductResponse
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string CategoryName  { get; set; }
        public decimal UnitPrice { get; set; }
        public int TotalSold { get; set; }
    }
}
