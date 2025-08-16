using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceMVC.Entities.Models
{
    public class CustomerProfile : BaseClass
    {
       public int ProfileId { get; set; }
      public int CustomerId { get; set; }
      public string FullName { get; set; }
      public string Address { get; set; }
      public string City { get; set; }
      public string PostalCode { get; set; }
       public string PhoneNumber { get; set; }
    }
}
