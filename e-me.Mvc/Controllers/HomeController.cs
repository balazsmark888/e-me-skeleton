using Microsoft.AspNetCore.Mvc;

namespace e_me.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
