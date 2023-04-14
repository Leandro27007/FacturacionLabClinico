using Microsoft.AspNetCore.Mvc;
using PrototipoHerasme.Models;
using System.Diagnostics;

namespace PrototipoHerasme.Controllers
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
            return View();
        }

    }
}