using Microsoft.EntityFrameworkCore;
using ToGoodToGo.Core;
using ToGoodToGo.Core.Models.Domains;
using ToGoodToGo.Core.Service;

namespace ToGoodToGo.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Order> GetAllOrdersForRestaurant(string userId)
        {
            return _unitOfWork.Order.GetAllOrdersForRestaurant(userId);
        }

        public IEnumerable<Order> GetAllOrdersForEndUser(string userId)
        {
            return _unitOfWork.Order.GetAllOrdersForEndUser(userId);
        }
        public void AddOrder(int foodPackageId, string endUserId)
        {
            _unitOfWork.Order.AddOrder(foodPackageId, endUserId);
            _unitOfWork.Complete();
        }

        public void DeleteOrderByRestaurant(int id, string userId)
        {
            _unitOfWork.Order.DeleteOrderByRestaurant(id, userId);
            _unitOfWork.Complete();
        }

        public void MarkOrderAsReceived(int id, string userId)
        {
            _unitOfWork.Order.MarkOrderAsReceived(id, userId);
            _unitOfWork.Complete(); 
        }

        public IEnumerable<Order> GetOrdersWithFilters(string userId,
            string firstName = null,
            string lastName = null,
            string status = null)
        {
            return _unitOfWork.Order.GetOrdersWithFilters(userId, firstName, lastName, status);
        }
    }
}
