using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToGoodToGo.Core.Models.Domains
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Data zamówienia")]
        public DateTime DateOfOrder { get; set; }

        [Display(Name = "Data odebrania")]
        public DateTime? DateOfReceipt { get; set; }

        [Display(Name = "Użytkownik")]
        public string EndUserId { get; set; }

        [Required(ErrorMessage = "Pole Paczka żywności jest wymagane")]     
        [Display(Name = "Paczka żywności")]
        public int FoodPackageId { get; set; }

        public FoodPackage FoodPackage { get; set; }
        
        public ApplicationUser EndUser { get; set; }
    }
}
