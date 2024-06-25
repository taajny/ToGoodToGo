using Microsoft.EntityFrameworkCore;
using ToGoodToGo.Core.Models.Domains;

namespace ToGoodToGo.Core
{
    public interface IApplicationDbContext
    {
        DbSet<FoodPackage> FoodPackages { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<ApplicationUser> ApplicationUsers { get; set; }

        int SaveChanges();
    }
}
