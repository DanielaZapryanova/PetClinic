using Microsoft.AspNetCore.Mvc;
using PetClinic.Models;
using System.Diagnostics;

namespace PetClinic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult ForUs()
        {
            return View("ForUs");
        }

        public IActionResult Contacts()
        {
            return View("Contacts");
        }

        public IActionResult ImportantInformation()
        {
            return View("ImportantInformation");
        }

        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        public IActionResult HomeVisits()
        {
            return View("HomeVisits");
        }
        public IActionResult PriceList()
        {
            return View("PriceList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}