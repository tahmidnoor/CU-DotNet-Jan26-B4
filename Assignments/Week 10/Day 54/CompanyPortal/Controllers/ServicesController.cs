using Microsoft.AspNetCore.Mvc;

namespace CompanyPortal.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Consulting()
        {
            return View();
        }
        public IActionResult Training()
        {
            return View();
        }
    }
}
