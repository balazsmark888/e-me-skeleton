using System.Linq;
using e_me.Model.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace e_me.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Json(_unitOfWork.SettingRepository.GetAll().ToList());
        }
    }
}
