using Microsoft.AspNetCore.Mvc;
using School.Helpers;
using School.Models;
using System.Diagnostics;

namespace School.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserSessionHelper _userSessionHelper;
        public HomeController(ILogger<HomeController> logger, UserSessionHelper userSessionHelper)
        {
            _logger = logger;
            _userSessionHelper = userSessionHelper;
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
