using ToGoodToGo.Core.Models.Domains;

namespace ToGoodToGo.Core.Service
{
    public interface IFoodPackageService
    {
        IEnumerable<FoodPackage> GetAllFoodPackages(string userId, bool alsoAfterDateOfExpiration = false);
        FoodPackage GetFoodPackage(int id, string userId);
        void Add(FoodPackage foodPackage);
        void Update(FoodPackage foodPackage);
        void DeleteFoodPackage(int id, string userId);
        string GetOldImageName(int id, string userId);
        IEnumerable<FoodPackage> GetFoodPackagesWithFilters(string userId, string title = null, string description = null, bool alsoAfterDateOfExpiration = false);
        IEnumerable<FoodPackage> GetAllFreeFoodPackages();
        FoodPackage GetFoodPackageDetails(int id);
    }
}
