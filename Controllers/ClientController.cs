using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ToGoodToGo.Core.Models;
using ToGoodToGo.Core.Models.Domains;
using ToGoodToGo.Core.Service;
using ToGoodToGo.Core.ViewModels;
using ToGoodToGo.Persistence;
using ToGoodToGo.Persistence.Extensions;
using ToGoodToGo.Persistence.Repositories;
using ToGoodToGo.Persistence.Services;

namespace ToGoodToGo.Controllers
{
    [Authorize(Roles = "Klient")]
    public class ClientController : Controller
    {
        private IFoodPackageService _foodPackageService;
        private IOrderService _orderService;

        public ClientController(IFoodPackageService foodPackageService, IOrderService orderService)
        {
            _foodPackageService = foodPackageService;
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            var userId = User.GetUserId;

            var vm = new FoodPackagesViewModel()
            {
                FoodPackages = _foodPackageService.GetAllFreeFoodPackages()
            };

            return View(vm);
        }

        public IActionResult FoodPackage(int id)
        {
            FoodPackage vm = _foodPackageService.GetFoodPackageDetails(id);

            return View(vm);
        }

        public IActionResult AddOrder(int id)
        {
            var userId = User.GetUserId();

           _orderService.AddOrder(id, userId);
          
            return RedirectToAction("Orders");
        }

        public IActionResult Orders() 
        { 
            var userId = User.GetUserId();
            var vm = new ClientOrdersViewModel()
            {
                Orders = _orderService.GetAllOrdersForEndUser(userId)
            };

            return View(vm);
        }
    }
}
