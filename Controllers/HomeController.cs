using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToGoodToGo.Core.Models.Domains;
//using ToGoodToGo.Core.Models;
using ToGoodToGo.Core.ViewModels;

namespace ToGoodToGo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //_userManager = userManager;
        }

        [Authorize(Roles = "Restauracja,Klient")]
        public IActionResult Index()
        {
            if (User.IsInRole("Restauracja"))
            {
                return RedirectToAction("Index", "Restaurant");
            }
            else if (User.IsInRole("Klient"))
            {
                return RedirectToAction("Index", "Client");
            }

            return View();


        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}