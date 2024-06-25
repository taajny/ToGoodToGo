
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using ToGoodToGo.Core;
using ToGoodToGo.Core.Models.Domains;
using ToGoodToGo.Core.Repositories;

namespace ToGoodToGo.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private IApplicationDbContext _context;
        public OrderRepository(IApplicationDbContext context)
        {
            _context = context; 
        }
        public IEnumerable<Order> GetAllOrdersForRestaurant(string userId)
        {
            var orders = _context.Orders
                .Include(y => y.EndUser)
                .Include(z => z.FoodPackage)
                .Where(y => y.FoodPackage.RestaurantId == userId);
            
            return orders.OrderBy(x => x.DateOfOrder).ToList();
        }

        public IEnumerable<Order> GetAllOrdersForEndUser(string userId)
        {
            var orders = _context.Orders
                .Include(y => y.EndUser)
                .Include(z => z.FoodPackage)
                .Include(v => v.FoodPackage.Restaurant)
                .Where(y => y.EndUser.Id == userId);

            return orders.OrderBy(x => x.DateOfOrder).ToList();
        }
        public void AddOrder(int foodPackageId, string endUserId)
        {
            var order = new Order()
            {
                DateOfOrder = DateTime.Now,
                EndUserId = endUserId,
                FoodPackageId = foodPackageId

            };

            _context.Orders.Add(order);
        }

        public void DeleteOrderByRestaurant(int id, string userId)
        {
            var orderToDelete = _context.Orders
                .Include(y => y.FoodPackage)
                .Include(z => z.EndUser)
                .Single(x => x.Id == id);
            
            if (orderToDelete.FoodPackage.RestaurantId == userId)
            {
                _context.Orders.Remove(orderToDelete);
                
            }      
        }

        public void MarkOrderAsReceived(int id, string userId)
        {
            var orderToMark = _context.Orders
                .Include(y => y.FoodPackage)
                .Single(x => x.Id == id);

            orderToMark.DateOfReceipt = DateTime.Now;

            if (orderToMark.FoodPackage.RestaurantId == userId)
            {
                _context.Orders.Update(orderToMark);
            }
        }

        public IEnumerable<Order> GetOrdersWithFilters(string userId, 
            string firstName = null, 
            string lastName = null, 
            string status = null)
        {
            var orders = _context.Orders
                .Include(y => y.FoodPackage)
                .Include(x => x.EndUser)
                .Where(y => y.FoodPackage.RestaurantId == userId);

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                orders = orders
                    .Where(x => x.EndUser.FirstName.Contains(firstName));
            }
            
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                orders = orders
                    .Where(x => x.EndUser.LastName.Contains(lastName));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                if (status == "Nieodebrane")
                {
                    orders = orders
                    .Where(x => x.DateOfReceipt == null);
                }
                else if (status == "Odebrane")
                {
                    orders = orders
                    .Where(x => x.DateOfReceipt != null);
                }
                
            }

            return orders.OrderBy(x => x.DateOfOrder).ToList();
        }
    }
}