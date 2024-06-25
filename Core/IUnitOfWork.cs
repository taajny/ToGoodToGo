using ToGoodToGo.Core.Repositories;
using ToGoodToGo.Persistence.Repositories;

namespace ToGoodToGo.Core
{
    public interface IUnitOfWork
    {
        IFoodPackageRepository FoodPackage { get; set; }
        IOrderRepository Order { get; set; }

        void Complete();
    }
}
