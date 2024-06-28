using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using ToGoodToGo.Core.Models.Domains;
using ToGoodToGo.Core.Service;
using ToGoodToGo.Core.ViewModels;
using ToGoodToGo.Persistence;
using ToGoodToGo.Persistence.Extensions;
using ToGoodToGo.Persistence.Repositories;
using ToGoodToGo.Persistence.Services;

namespace ToGoodToGo.Controllers
{
    [Authorize(Roles = "Restauracja")]
    public class RestaurantController : Controller
    {
        private IFoodPackageService _foodPackageService;
        private IOrderService _orderService;
        
        private IWebHostEnvironment _environment;
        public RestaurantController(IFoodPackageService foodPackageService, IOrderService orderService, IWebHostEnvironment environment)
        {
            _foodPackageService = foodPackageService; 
            _orderService = orderService;

            _environment = environment;
        }

        
        public IActionResult Index(int section = 0)
        {
            //section - określa które sekcje będą wyświetlane:
            // 0 - wszystkie (zamówienia oraz paczki z żywnością)
            // 1 - same paczki z żywnością
            // 2 - same zamówienia

            var userId = User.GetUserId();
            var vm = new RestaurantViewModel()
            {
                FoodPackages = _foodPackageService.GetAllFoodPackages(userId),
                Orders = _orderService.GetAllOrdersForRestaurant(userId),
                WhichSection = section
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(RestaurantViewModel viewModel)
        {
            var userId = User.GetUserId();

            if (viewModel.FilterFoodPackages is not null)
            {
                var foodPackages = _foodPackageService.GetFoodPackagesWithFilters(userId,
                viewModel.FilterFoodPackages.Title,
                viewModel.FilterFoodPackages.Description,
                viewModel.FilterFoodPackages.AlsoAfterDateOfExpiration);

                return PartialView("_FoodPackagesTable", foodPackages);
            }

            if (viewModel.FilterOrders is not null)
            {
                var orders = _orderService.GetOrdersWithFilters(userId,
                    viewModel.FilterOrders.FirstName,
                    viewModel.FilterOrders.LastName,
                    viewModel.FilterOrders.Status);

                return PartialView("_OrdersTable", orders);
            }

            return View(viewModel);
            
        }
            
        public IActionResult FoodPackage(int id = 0)
        {
            var userId = User.GetUserId();

            var foodPackage = id == 0 ?
                new FoodPackage
                {
                    Id = 0,
                    RestaurantId = userId,
                    DateOfCreation = DateTime.Now,
                    ExpirationDate = DateTime.Now,
                } :
                _foodPackageService.GetFoodPackage(id, userId);

            var vm = new FoodPackageViewModel()
            {
                FoodPackage = foodPackage,
                Heading = id == 0 ? "Dodawanie nowej paczki z żywnością" : "Edycja istniejącej paczki z żywnością"
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FoodPackage(FoodPackageViewModel model)
        {
            var userId = User.GetUserId();
            model.FoodPackage.RestaurantId = userId;

            if (!ModelState.IsValid)
            {
                var vm = new FoodPackageViewModel()
                {
                    FoodPackage = model.FoodPackage,
                    Heading = model.FoodPackage.Id == 0 ? "Dodawanie nowej paczki z żywnością" : "Edycja istniejącej paczki z żywnością"
                };

                return View("FoodPackage", vm);
            }

            
            if (model.FoodPackage.Id == 0)
            {
                if (model.Photo is null)
                {
                    model.FoodPackage.ImagePath = "";
                }
                else
                {
                    model.FoodPackage.ImagePath = model.Photo.FileName;
                    CopyImage(model.Photo);
                }
                
                _foodPackageService.Add(model.FoodPackage);
            }
            else
            {
                if (model.Photo is null)
                { 
                    model.FoodPackage.ImagePath = _foodPackageService.GetOldImageName(model.FoodPackage.Id, userId);
                }
                else
                { 
                    model.FoodPackage.ImagePath = model.Photo.FileName;
                    CopyImage(model.Photo);
                }
                _foodPackageService.Update(model.FoodPackage);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteFoodPackage(int id) 
        {
            try
            {
                var userId = User.GetUserId();
                _foodPackageService.DeleteFoodPackage(id, userId);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _orderService.DeleteOrderByRestaurant(id, userId);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult MarkOrderAsReceived(int id)
        {
            var userId = User.GetUserId();

            _orderService.MarkOrderAsReceived(id, userId);

            return RedirectToAction("Index");
        }
            
        private void CopyImage(IFormFile photo)
        {
            string uploadFolder = Path.Combine(_environment.WebRootPath, "images");
            string filePath = Path.Combine(uploadFolder, photo.FileName);

            using(var fileStream = new FileStream(filePath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }
            
        }
    }
}
