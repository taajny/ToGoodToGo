using ToGoodToGo.Core.Models.Domains;

namespace ToGoodToGo.Core.Service
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrdersForRestaurant(string userId);
        IEnumerable<Order> GetAllOrdersForEndUser(string userId);
        void AddOrder(int foodPackageId, string endUserId);
        void DeleteOrderByRestaurant(int id, string userId);
        void MarkOrderAsReceived(int id, string userId);
        IEnumerable<Order> GetOrdersWithFilters(string userId, string firstName = null, string lastName = null, string status = null);
    }
}
