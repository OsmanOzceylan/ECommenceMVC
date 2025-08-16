﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceMVC.Core.Models.Request
{
    public class CheckoutRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CVV { get; set; }

    }
}
