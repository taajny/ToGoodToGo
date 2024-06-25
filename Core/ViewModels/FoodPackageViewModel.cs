using ToGoodToGo.Core.Models.Domains;

namespace ToGoodToGo.Core.ViewModels
{
    public class FoodPackageViewModel
    {
        public string Heading { get; set; }
        public FoodPackage FoodPackage { get; set; }
        public IFormFile Photo { get; set; }
    }
}
