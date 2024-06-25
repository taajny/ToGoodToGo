using Microsoft.EntityFrameworkCore;
using ToGoodToGo.Core;
using ToGoodToGo.Core.Models.Domains;
using ToGoodToGo.Core.Service;

namespace ToGoodToGo.Persistence.Services
{
    public class FoodPackageService : IFoodPackageService
    {
        private IUnitOfWork _unitOfWork;
        public FoodPackageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<FoodPackage> GetAllFoodPackages(string userId, bool alsoAfterDateOfExpiration = false)
        {
            return _unitOfWork.FoodPackage.GetAllFoodPackages(userId, alsoAfterDateOfExpiration);
        }

        public FoodPackage GetFoodPackage(int id, string userId)
        {
            return _unitOfWork.FoodPackage.GetFoodPackage(id, userId);
        }

        public void Add(FoodPackage foodPackage)
        {
            _unitOfWork.FoodPackage.Add(foodPackage);
            _unitOfWork.Complete();
        }
        public void Update(FoodPackage foodPackage)
        {
            _unitOfWork.FoodPackage.Update(foodPackage);
            _unitOfWork.Complete();
        }

        public void DeleteFoodPackage(int id, string userId)
        {
            _unitOfWork.FoodPackage.DeleteFoodPackage(id, userId);
            _unitOfWork.Complete();
        }

        public string GetOldImageName(int id, string userId)
        {
            return _unitOfWork.FoodPackage.GetOldImageName(id, userId);
        }

        public IEnumerable<FoodPackage> GetFoodPackagesWithFilters(string userId,
            string title = null,
            string description = null,
            bool alsoAfterDateOfExpiration = false)
        {
            return _unitOfWork.FoodPackage.GetFoodPackagesWithFilters(userId, title, description, alsoAfterDateOfExpiration);
        }

        public IEnumerable<FoodPackage> GetAllFreeFoodPackages()
        {
            return _unitOfWork.FoodPackage.GetAllFreeFoodPackages();
        }

        public FoodPackage GetFoodPackageDetails(int id)
        {
            return _unitOfWork.FoodPackage.GetFoodPackageDetails(id);       
        }
    }
}
