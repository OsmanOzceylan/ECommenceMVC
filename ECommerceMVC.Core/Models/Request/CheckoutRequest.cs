using System.ComponentModel.DataAnnotations;

namespace ECommerceMVC.Core.Models.Request
{
    public class CheckoutRequest
    {
        [Required(ErrorMessage = "Ad zorunludur.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Adres zorunludur.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Şehir zorunludur.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Ülke zorunludur.")]
        public string Country { get; set; }
        public string? District { get; set; }

        [Required(ErrorMessage = "Posta kodu zorunludur.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Kart numarası zorunludur.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Kart üzerindeki isim zorunludur.")]
        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "CVV zorunludur.")]
        public string CVV { get; set; }
    }
}
