using ToGoodToGo.Core;
using ToGoodToGo.Core.Repositories;
using ToGoodToGo.Persistence.Repositories;

namespace ToGoodToGo.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;
        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context;

            FoodPackage = new FoodPackageRepository(_context);
            Order = new OrderRepository(_context);
        }
    
        public IFoodPackageRepository FoodPackage { get; set; }
        public IOrderRepository Order { get; set; }

        public void Complete()
        {
            _context.SaveChanges();
        }

    }
}
