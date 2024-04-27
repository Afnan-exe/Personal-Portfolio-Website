using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using System.Diagnostics;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly _context _Context;

        public HomeController(ILogger<HomeController> logger, _context context)
        {
            _logger = logger;
            _Context = context;
        }

        public IActionResult Index()
        {
            return View(_Context.Project.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

		public IActionResult Dashboard()
		{

            if (HttpContext.Session.GetString("i") == null)
            {
                return RedirectToAction("Index", "Home");
            }
                return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
