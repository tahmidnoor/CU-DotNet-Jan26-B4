using Microsoft.AspNetCore.Mvc;

namespace TrainPortal.Controllers
{
    public class TrainController : Controller
    {
        public IActionResult Courses()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }
    }
}
