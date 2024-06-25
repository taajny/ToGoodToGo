using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ToGoodToGo.Core.Models.Domains
{
    public class FoodPackage
    {
        public FoodPackage()
        {
            Orders = new Collection<Order>();
        }
        public int Id { get; set; }
        
        [Display(Name = "Tytuł")]
        [Required(ErrorMessage = "Pole Tytuł jest wymagane.")]
        [MaxLength(50)]
        public string Title { get; set; }
        
        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Pole Opis jest wymagane.")]
        [MaxLength(255)]
        public string Descrirption { get; set; }
        
        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Pole Cena jest wymagane.")]
        public decimal Price { get; set; }
        
        [Display(Name = "Zdjęcie")]
        [MaxLength(255)]
        public string ImagePath { get; set; }
        
        [Display(Name = "Data utworzenia")]
        public DateTime DateOfCreation { get; set; }
        
        [Display(Name = "Data ważności")]
        [Required(ErrorMessage = "Pole Data ważności jest wymagane.")]
        public DateTime ExpirationDate { get; set; }
        
        [Display(Name = "Restauracja")]
        public string RestaurantId { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ApplicationUser Restaurant { get; set; }
        
    }
}
