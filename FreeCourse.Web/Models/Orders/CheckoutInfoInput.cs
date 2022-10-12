using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Orders
{
    public class CheckoutInfoInput
    {
        [Display(Name = "il")]
        public string Province { get; set; }
        [Display(Name = "ilçe")]
        public string District { get; set; }
        [Display(Name = "Cadde")]
        public string Street { get; set; }
        [Display(Name = "Posta Kodu")]
        public string ZipCode { get; set; }
        [Display(Name = "Address")]
        public string Line { get; set; }
        [Display(Name = "Kart isim soy isim")]
        public string CardName { get; set; }
        [Display(Name = "Kart Numarası")]
        public string CardNumber { get; set; }
        [Display(Name = "son kullanma tarihi")]
        public string Expiration { get; set; }
        [Display(Name = "CVV/CVC2")]
        public string CVV { get; set; }
    }
}
