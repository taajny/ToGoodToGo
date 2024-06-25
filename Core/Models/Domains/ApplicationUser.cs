using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ToGoodToGo.Core.Models.Domains;

namespace ToGoodToGo.Core.Models.Domains
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            FoodPackages = new Collection<FoodPackage>();
            Orders = new Collection<Order>();
        }
        public ICollection<FoodPackage> FoodPackages { get; set; }
        public ICollection<Order> Orders { get; set; }

        [Display(Name = "Nazwa restauracji")]
        [MaxLength(50)]
        public string RestaurantName { get; set; }
        
        [Display(Name = "Imię")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Pole Imię jest wymagane.")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Pole Nazwisko jest wymagane.")]
        public string LastName { get; set; }

        [Display(Name = "Adres (ulica i numer domu)")]
        [MaxLength(80)]
        [Required(ErrorMessage = "Pole Adres jest wymagane.")]
        public string Street { get; set; }

        [Display(Name = "Miasto")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Pole Miasto jest wymagane.")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Pole Kod pocztowy jest wymagane.")]
        public string PostalCode { get; set; }
        public bool IsRestaurant { get; set; }
        public bool IsEndUser { get; set; }
    }
}
