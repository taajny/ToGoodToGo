using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using ToGoodToGo.Core;
using ToGoodToGo.Core.Models.Domains;
using ToGoodToGo.Core.Repositories;

namespace ToGoodToGo.Persistence.Repositories
{
    public class FoodPackageRepository : IFoodPackageRepository
    {
        private IApplicationDbContext _context;
        
        public FoodPackageRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<FoodPackage> GetAllFoodPackages(string userId, bool alsoAfterDateOfExpiration = false)
        {
            var foodPackages = _context.FoodPackages
                .Include(x => x.Orders)
                .Where(x => x.RestaurantId == userId);

            if (!alsoAfterDateOfExpiration)
            {
                foodPackages = foodPackages
                    .Where(x => x.ExpirationDate > DateTime.Now);
            }

            return foodPackages.OrderBy(x => x.DateOfCreation).ToList();
        }

        public FoodPackage GetFoodPackage(int id, string userId)
        {
            return _context.FoodPackages.Single(x => x.Id == id && x.RestaurantId == userId) as FoodPackage;
        }

        public void Add(FoodPackage foodPackage)
        {
            foodPackage.DateOfCreation = DateTime.Now;
            _context.FoodPackages.Add(foodPackage);
        }
        public void Update(FoodPackage foodPackage)
        {
            var foodPackageToUpdate = _context.FoodPackages.Single(x => x.Id == foodPackage.Id);


            foodPackageToUpdate.Title = foodPackage.Title;
            foodPackageToUpdate.Descrirption = foodPackage.Descrirption;
            foodPackageToUpdate.Price = foodPackage.Price;
            foodPackageToUpdate.ImagePath = foodPackage.ImagePath;
            foodPackageToUpdate.ExpirationDate = foodPackage.ExpirationDate;

            _context.FoodPackages.Update(foodPackageToUpdate);
        }

        public void DeleteFoodPackage(int id, string userId)
        {
            var foodPackageToDelete = _context.FoodPackages.Single(x => x.Id == id && x.RestaurantId == userId);

            _context.FoodPackages.Remove(foodPackageToDelete);
        }

        public string GetOldImageName(int id, string userId)
        {
            var foodPackage = _context.FoodPackages.Single(x => x.Id == id && x.RestaurantId == userId);

            return foodPackage.ImagePath;
        }

        public IEnumerable<FoodPackage> GetFoodPackagesWithFilters(string userId, 
            string title = null, 
            string description = null, 
            bool alsoAfterDateOfExpiration = false)
        {
            var foodPackages = _context.FoodPackages
                .Where(x => x.RestaurantId == userId);

            if (!alsoAfterDateOfExpiration)
            {
                foodPackages = foodPackages
                    .Where(x => x.ExpirationDate.Day >= DateTime.UtcNow.Day);
            }
          
            if (!string.IsNullOrWhiteSpace(title))
            {
                foodPackages = foodPackages
                    .Where(x => x.Title.Contains(title));
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                foodPackages = foodPackages
                    .Where(x => x.Descrirption.Contains(description));
            }

            return foodPackages.OrderBy(x => x.DateOfCreation).ToList();
        }

        public IEnumerable<FoodPackage> GetAllFreeFoodPackages()
        {
            var foodPackages = _context.FoodPackages
                .Include(x => x.Restaurant)
                .Include(y => y.Orders)
                .Where(x => x.ExpirationDate > DateTime.Now && x.Orders.Count == 0);          

            return foodPackages.OrderBy(x => x.DateOfCreation).ToList();
        }

        public FoodPackage GetFoodPackageDetails(int id)
        {
            var foodPackage = _context.FoodPackages
                .Include(x => x.Restaurant)
                .Single(x => x.Id == id);

            return foodPackage;
        }
    }
}