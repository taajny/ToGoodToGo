using ToGoodToGo.Core.Models;
using ToGoodToGo.Core.Models.Domains;

namespace ToGoodToGo.Core.ViewModels
{
    public class RestaurantViewModel
    {
        public IEnumerable<FoodPackage> FoodPackages { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public FilterFoodPackages FilterFoodPackages { get; set; }
        public FilterOrders FilterOrders { get; set; }
        public int WhichSection { get; set; }
    }
}
