using Restaurant.Web.Models.EmployeeModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Restaurant.Web.Controllers
{
    public class HomeEmployeeController : Controller
    {
        private readonly ILogger<HomeEmployeeController> _logger;

        public HomeEmployeeController(ILogger<HomeEmployeeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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
