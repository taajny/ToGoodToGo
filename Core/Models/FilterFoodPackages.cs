using System.ComponentModel.DataAnnotations;

namespace ToGoodToGo.Core.Models
{
    public class FilterFoodPackages
    {
        public string Title { get; set; }
        public string Description { get; set; }

        [Display(Name = "Pokaż również przeterminowane")]
        public bool AlsoAfterDateOfExpiration { get; set; }
    }
}
    