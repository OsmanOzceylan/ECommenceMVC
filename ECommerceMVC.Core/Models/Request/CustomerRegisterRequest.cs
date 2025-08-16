using System.ComponentModel.DataAnnotations;

namespace ECommerceMVC.Core.Models.Request
{
    public class CustomerRegisterRequest
    {
        [Required(ErrorMessage = "Müşteri adı zorunludur.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Müşteri şifresi zorunludur.")]
        public string CustomerPassword { get; set; }
    }
}
