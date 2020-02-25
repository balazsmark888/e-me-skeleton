using System.Diagnostics;
using e_me.server.Mvc.Data;
using e_me.server.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace e_me.server.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        protected ApplicationDbContext ApplicationContext { get; set; }

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationContext)
        {
            ApplicationContext = applicationContext;
            _logger = logger;
        }

        public IActionResult Index()
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
